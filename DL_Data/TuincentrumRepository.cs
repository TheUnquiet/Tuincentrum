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
using BL.Models.DTOS;

namespace DL_Data
{
    public class TuincentrumRepository : ITuincentrumRepository
    {
        private string connectionString;

        public TuincentrumRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool HeeftBestelling(Bestelling bestelling)
        {
            string SQL = "select count(*) from bestelling where offerte_id=@offerte_id and product_id=@product_id";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = SQL;
                    cmd.Parameters.Add(new SqlParameter("@offerte_id", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@product_id", SqlDbType.Int));
                    cmd.Parameters["@offerte_id"].Value = bestelling.Offerte_Id;
                    cmd.Parameters["@product_id"].Value = bestelling.Product_Id;
                    int count = (int)cmd.ExecuteScalar();

                    if (count > 0) return true; return false;
                } catch (Exception ex) { throw new TuincentrumException($"HeeftBestelling - {ex.Message}", ex); }
            }
        }

        public void SchrijfBestelling(Bestelling bestelling)
        {
            string SQL = "insert into Bestelling(offerte_id, product_id, aantal) values(@offerte_id, @product_id, @aantal)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = SQL;
                    cmd.Parameters.Add(new SqlParameter("@offerte_id", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@product_id", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@aantal", SqlDbType.Int));
                    cmd.Parameters["@offerte_id"].Value = bestelling.Offerte_Id;
                    cmd.Parameters["@product_id"].Value = bestelling.Product_Id;
                    cmd.Parameters["@aantal"].Value = bestelling.Aantal_Product;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex) { throw new TuincentrumException($"SchijfKlant -{ex.Message} "); }
            }
        }

        public bool HeeftKlant(Klant klant)
        {
            string SQL = "select count(*) from klant where naam=@naam and adres=@adres";
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

                    int count = (int)cmd.ExecuteScalar();
                    if (count > 0) return true; return false;
                } catch (Exception ex) { throw new TuincentrumException($"HeeftKlant -{ex.Message} "); }
            }
        }

        public void SchrijfKlant(Klant klant)
        {
            string SQL = "insert into klant(naam, adres) values(@naam, @adres)";
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

        public bool HeeftOfferte(Offerte offerte)
        {
            string SQL = "select count(*) from offerte where datum=@datum and klantnummer=@klantnummer and afhaal=@afhaal and aanleg=@aanleg and aantal_producten=@aantal_producten";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = SQL;
                    cmd.Parameters.Add(new SqlParameter("@datum", SqlDbType.Date));
                    cmd.Parameters.Add(new SqlParameter("@klantnummer", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@afhaal", SqlDbType.Bit));
                    cmd.Parameters.Add(new SqlParameter("@aanleg", SqlDbType.Bit));
                    cmd.Parameters.Add(new SqlParameter("@aantal_producten", SqlDbType.Int));
                    cmd.Parameters["@datum"].Value = offerte.Datum;
                    cmd.Parameters["@klantnummer"].Value = offerte.KlantNummer;
                    cmd.Parameters["@aanleg"].Value = offerte.Aanleg;
                    cmd.Parameters["@afhaal"].Value = offerte.Afhaal;
                    cmd.Parameters["@aantal_producten"].Value = offerte.AantalProducten;

                    int count = (int)cmd.ExecuteScalar();
                    if (count > 0) return true; return false;
                } catch (Exception ex) { throw new TuincentrumException($"HeeftOfferte - {ex.Message}", ex); }
            }
        }

        public void SchrijfOfferte(Offerte offerte)
        {
            string SQL = "insert into offerte(datum, afhaal, klantnummer, aanleg, aantal_producten) values(@datum, @afhaal, @klantnr, @aanleg, @aantal_producten)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = SQL;
                    cmd.Parameters.Add(new SqlParameter("@datum", SqlDbType.Date));
                    cmd.Parameters.Add(new SqlParameter("@klantNr", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@afhaal", SqlDbType.Bit));
                    cmd.Parameters.Add(new SqlParameter("@aanleg", SqlDbType.Bit));
                    cmd.Parameters.Add(new SqlParameter("@aantal_producten", SqlDbType.Int));
                    cmd.Parameters["@datum"].Value = offerte.Datum;
                    cmd.Parameters["@klantnr"].Value = offerte.KlantNummer;
                    cmd.Parameters["@afhaal"].Value = offerte.Afhaal;
                    cmd.Parameters["@aanleg"].Value = offerte.Aanleg;
                    cmd.Parameters["@aantal_producten"].Value = offerte.AantalProducten;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex) { throw new TuincentrumException($"SchijfOfferte -{ex.Message} "); }
            }
        }

        public bool HeeftProduct(Product product)
        {
            string SQL = "select count(*) from product where naam_nl=@naam_nl and naam_w=@naam_w and prijs=@prijs and beschrijving=@beschrijving";
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

                    int count = (int)cmd.ExecuteScalar();
                    if (count > 0) return true; return false;
                }
                catch (Exception ex) { throw new TuincentrumException($"HeeftProduct - {ex.Message}", ex); }
            }
        }

        public void SchrijfProduct(Product product)
        {
            string SQL = "insert into product(naam_nl, naam_w, prijs, beschrijving) values(@naam_nl, @naam_w, @prijs, @beschrijving)";
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
            string SQL = "select k.id, k.naam, k.adres, o.id as 'offerte_id' from klant k left join offerte o on o.klantnummer = k.id where k.naam like @naam";
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
                    if (!klanten.ContainsKey((int)reader["id"]))
                    {
                        klanten.Add((int)reader["id"], new Klant(
                            (int)reader["id"], 
                            (string)reader["naam"],
                            (string)reader["adres"]));
                    }

                    klanten[(int)reader["id"]].Offertes.Add(new OfferteInfo((int)reader["offerte_id"], BerekenPrijs((int)reader["offerte_id"])));
                }
            }

            return klanten.Values.ToList();
        }

        public Offerte LeesOfferte(int id)
        {
            Offerte offerte = null;
            string SQL = "select o.id, o.datum, o.klantnummer, o.afhaal, o.aanleg, o.aantal_producten from offerte o where o.id=@offerte_id";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = SQL;
                cmd.Parameters.Add("@offerte_id", SqlDbType.Int);
                cmd.Parameters["@offerte_id"].Value= id;
                IDataReader reader = cmd.ExecuteReader();
                
                while (reader.Read()) 
                {
                    offerte = new(
                        (int)reader["id"],
                        (DateTime)reader["datum"],
                        (int)reader["klantnummer"],
                        (bool)reader["afhaal"],
                        (bool)reader["aanleg"],
                        (int)reader["aantal_producten"]);
                }
            }

            return offerte;
        }

        public List<Product> LeesProductenOfferte(int offerteId)
        {

            List<Product> producten = new List<Product>();
            string SQL = "select p.id, p.naam_nl, p.naam_w, p.prijs, p.beschrijving from offerte o\r\njoin bestelling b on o.id = b.offerte_id\r\njoin product p on p.id = b.product_id\r\nwhere o.id = @offerte_id;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = SQL;
                cmd.Parameters.Add("@offerte_id", SqlDbType.Int);
                cmd.Parameters["@offerte_id"].Value = offerteId;
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Product product = new(
                            (int)reader["id"],
                            (string)reader["naam_nl"],
                            (string)reader["naam_w"],
                            (double)reader["prijs"],
                            (string)reader["beschrijving"]);
                    producten.Add(product);
                }

                return producten;
            }
        }

        public Product LeesProduct(int id)
        {
            Product product = null;
            string SQL = "select * from product where id=@id";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = SQL;
                cmd.Parameters.Add("@id", SqlDbType.Int);
                cmd.Parameters["@id"].Value = id;
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    product = new(
                        (int)reader["id"],
                        (string)reader["naam_nl"],
                        (string)reader["naam_w"],
                        (double)reader["prijs"],
                        (string)reader["beschrijving"]);
                }
                return product;
            }
        }

        public double BerekenPrijs(int offerte_id)
        {
            double prijs = 0;
            string SQL = "select round(\r\n    case\r\n        when sum(p.prijs * b.aantal) > 5000 then sum(p.prijs * b.aantal) * 0.9\r\n        when sum(p.prijs * b.aantal) > 2000 then sum(p.prijs * b.aantal) * 0.95\r\n        else sum(p.prijs * b.aantal)\r\n    end\r\n    + case \r\n        when o.afhaal = 0 and sum(p.prijs * b.aantal) < 500 then 100\r\n        when o.afhaal = 0 and sum(p.prijs * b.aantal) between 500 and 1000 then 50\r\n        else 0\r\n    end\r\n    + case\r\n        when o.aanleg = 1 and sum(p.prijs * b.aantal) > 5000 then sum(p.prijs * b.aantal) * 0.05 \r\n        when o.aanleg = 1 and sum(p.prijs * b.aantal) > 2000 then sum(p.prijs * b.aantal) * 0.10\r\n        when o.aanleg = 1 and sum(p.prijs * b.aantal) <= 2000 then sum(p.prijs * b.aantal) * 0.15\r\n        else 0\r\n    end\r\n, 2) as totaal\r\nfrom bestelling b\r\njoin product p on p.id = b.product_id\r\njoin offerte o on o.id = b.offerte_id\r\nwhere b.offerte_id = @offerte_id group by o.afhaal, o.aanleg\r\n";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = SQL;
                cmd.Parameters.Add("@offerte_id", SqlDbType.Int);
                cmd.Parameters["@offerte_id"].Value = offerte_id;
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    prijs = (double)reader["totaal"];
                }
            }

            return prijs;
        }
    }
}
