using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Interfaces;
using BL.Models;
using BL.Exceptions;
using System.Numerics;

namespace DL_Data
{
    public class TuincentrumRepository : ITuincentrumRepository
    {
        private string connectionString;

        public TuincentrumRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void SchrijfKlant(Klant klant)
        {
            string SQL = "BEGIN TRANSACTION; BEGIN TRY INSERT INTO klant (naam, adres) VALUES (@naam, @adres) COMMIT END TRY BEGIN CATCH ROLLBACK; END CATCH;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = SQL;
                    cmd.Parameters.Add(new SqlParameter("@naam", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@adres", SqlDbType.NVarChar));
                    cmd.Parameters["@naam"].Value = klant.Naam;
                    cmd.Parameters["@adres"].Value = klant.Adres;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex) { throw new TuincentrumException($"SchijfKlant -{ex.Message} "); }
            }
        }

        public void SchrijfOfferteEnProducten(Offerte offerte)
        {
            string SQL = "BEGIN TRANSACTION; BEGIN TRY INSERT INTO offerte(datum, afhaal, klantnummer, aanleg, aantal_producten) VALUES(@datum, @afhaal, @klantNr, @aanleg, @aantal_producten); SELECT @offerte_id = SCOPE_IDENTITY();  COMMIT; END TRY BEGIN CATCH ROLLBACK; END CATCH;";

            string SQL_Bestelling = "BEGIN TRANSACTION; BEGIN TRY INSERT INTO bestelling(offerte_id, product_id, aantal) VALUES(@offerte_id, @product_id, @aantal) COMMIT; END TRY BEGIN CATCH ROLLBACK; END CATCH";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();

                    // pak offerte_id en upload de offerte
                    cmd.CommandText = SQL;
                    cmd.Parameters.Add(new SqlParameter("@datum", SqlDbType.Date)).Value = offerte.Datum;
                    cmd.Parameters.Add(new SqlParameter("@klantNr", SqlDbType.Int)).Value = offerte.Klant.Id;
                    cmd.Parameters.Add(new SqlParameter("@afhaal", SqlDbType.Bit)).Value = offerte.Afhaal;
                    cmd.Parameters.Add(new SqlParameter("@aanleg", SqlDbType.Bit)).Value = offerte.Aanleg;
                    cmd.Parameters.Add(new SqlParameter("@aantal_producten", SqlDbType.Int)).Value = offerte.AantalProducten;

                    // id van de nieuwe offerte 
                    SqlParameter offerteIdParam = new SqlParameter("@offerte_id", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(offerteIdParam);

                    cmd.ExecuteNonQuery();

                    // Pak de gegenereerde id
                    int offerte_id = (int)offerteIdParam.Value;

                    // Zet de producten in de tussen tabel
                    foreach (var p in offerte.Producten)
                    {
                        using (SqlCommand productCmd = conn.CreateCommand())
                        {
                            productCmd.CommandText = SQL_Bestelling;
                            productCmd.Parameters.Add(new SqlParameter("@offerte_id", SqlDbType.Int)).Value = offerte_id;
                            productCmd.Parameters.Add(new SqlParameter("@product_id", SqlDbType.Int)).Value = p.Key.Id;
                            productCmd.Parameters.Add(new SqlParameter("@aantal", SqlDbType.Int)).Value = p.Value;

                            productCmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new TuincentrumException($"SchijfOfferteEnProducten - {ex.Message}");
                }
            }
        }

        public void SchrijfProduct(Product product)
        {
            string SQL = "BEGIN TRANSACTION; \r\nBEGIN TRY \r\ninsert into product(naam_nl, naam_w, prijs, beschrijving) \r\nvalues(@naam_nl, @naam_w, @prijs, @beschrijving)\r\nCOMMIT\r\nEND TRY\r\nBEGIN CATCH\r\nROLLBACK;\r\nEND CATCH;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = SQL;
                    cmd.Parameters.Add(new SqlParameter("@naam_nl", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@naam_w", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@prijs", SqlDbType.Float));
                    cmd.Parameters.Add(new SqlParameter("@beschrijving", SqlDbType.NVarChar));
                    cmd.Parameters["@naam_nl"].Value = product.Naam_nl;
                    cmd.Parameters["@naam_w"].Value = product.Naam_W;
                    cmd.Parameters["@prijs"].Value = product.Prijs;
                    cmd.Parameters["@beschrijving"].Value = product.Beschrijving;
                    cmd.ExecuteNonQuery();
                }
                catch (TuincentrumException)
                {
                    return;
                }
                catch (Exception ex) { throw new TuincentrumException($"SchijfProduct -{ex.Message} ");
                }

            }
        }

        public List<Klant> LeesKlanten(string naam)
        {
            naam = $"%{naam}%";
            string SQL = "select k.id, k.naam, k.adres, o.id as 'offerte_id', o.datum, o.aanleg, o.afhaal, o.aantal_producten, o.klantnummer from klant k left join offerte o on o.klantnummer = k.id \r\nwhere k.naam like @naam";
            Dictionary<int, Klant> klanten = new();
            
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = SQL;
                cmd.Parameters.Add("@naam", SqlDbType.NVarChar);
                cmd.Parameters["@naam"].Value = naam;
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int klantId = (int)reader["id"];
                    if (!klanten.ContainsKey((int)reader["id"]))
                    {
                        klanten.Add((int)reader["id"], new Klant(
                            (int)reader["id"], 
                            (string)reader["naam"],
                            (string)reader["adres"]));
                    }

                    if (reader["offerte_id"] != DBNull.Value)
                    {
                        Offerte o = new Offerte(
                                (int)reader["offerte_id"],
                                (DateTime)reader["datum"],
                                (bool)reader["afhaal"],
                                (bool)reader["aanleg"],
                                (int)reader["aantal_producten"],
                                klanten[klantId]);
                        o.RekenAf();

                        klanten[klantId].Offertes.Add(o);
                    }
                }
            }

            return klanten.Values.ToList();
        }

        public List<Product> LeesProduct(string naam)
        {
            naam = $"%{naam}%";
            string SQL = "select * from product p where p.naam_nl like @naam";
            List<Product> producten = new();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = SQL;
                cmd.Parameters.Add("@naam", SqlDbType.NVarChar);
                cmd.Parameters["@naam"].Value = naam;
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    producten.Add(new Product(
                        (int)reader["id"],
                        (string)reader["naam_nl"],
                        (string)reader["naam_w"],
                        (double)reader["prijs"],
                        (string)reader["beschrijving"]
                    ));
                }
            }

            return producten;
        }

        public Offerte LeesOfferte(int id)
        {
            Offerte offerte = null;
            string SQL = "select o.id, o.datum, o.klantnummer, o.afhaal, o.aanleg, o.aantal_producten from offerte o where o.id=@offerte_id";

            string SQL_Klant = "select k.id, k.naam, k.adres from klant k\r\njoin offerte o on o.klantnummer = k.id where o.id=@offerte_id";

            string SQL_Producten = "select p.id, p.naam_nl, p.naam_w, p.prijs, p.beschrijving, b.aantal from offerte o\r\njoin bestelling b on o.id = b.offerte_id\r\njoin product p on p.id = b.product_id\r\nwhere o.id = @offerte_id;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                {
                    cmd.Parameters.Add("@offerte_id", SqlDbType.Int).Value = id;
                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            offerte = new Offerte(
                                (int)reader["id"],
                                (DateTime)reader["datum"],
                                (bool)reader["afhaal"],
                                (bool)reader["aanleg"],
                                (int)reader["aantal_producten"],
                                null);
                        }
                    }
                }

                if (offerte == null)
                {
                    return null;
                }

                using (SqlCommand cmd = new SqlCommand(SQL_Klant, conn))
                {
                    cmd.Parameters.Add("@offerte_id", SqlDbType.Int).Value = id;
                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Klant klant = new Klant(
                                (int)reader["id"],
                                (string)reader["naam"],
                                (string)reader["adres"]);
                            offerte.Klant = klant;
                        }
                    }
                }

                using (SqlCommand cmd = new SqlCommand(SQL_Producten, conn))
                {
                    cmd.Parameters.Add("@offerte_id", SqlDbType.Int).Value = id;
                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product product = new Product(
                                (int)reader["id"],
                                (string)reader["naam_nl"],
                                (string)reader["naam_w"],
                                (double)reader["prijs"],
                                (string)reader["beschrijving"]);
                            int aantal = (int)reader["aantal"];
                            offerte.VoegProduct(product, aantal);
                        }
                    }
                }
            }

            offerte.RekenAf();
            return offerte;
        }

        public List<Product> LeesAlleProducten()
        {
            List<Product> producten = new List<Product>();
            string SQL = "select * from product";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = SQL;
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    producten.Add(new Product(
                        (int)reader["id"],
                        (string)reader["naam_nl"],
                        (string)reader["naam_w"],
                        (double)reader["prijs"],
                        (string)reader["beschrijving"]));
                }

                return producten;
            }
        }
    }
}
