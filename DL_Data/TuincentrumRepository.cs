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
                catch (Exception ex) { throw new TuincentrumException($"SchijfKlant -{ex.Message} "); }
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
                catch (Exception ex) { throw new TuincentrumException($"SchijfKlant -{ex.Message} "); }
            }
        }

        public Dictionary<string, Klant> LeesKlanten()
        {
            string SQL = "select * from klant";
            Dictionary<string, Klant> klanten = new Dictionary<string, Klant>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = SQL;
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string naamAdres = (string)reader["naam"] + (string)reader["adres"];
                    klanten.Add(naamAdres, new Klant((int)reader["id"], (string)reader["naam"], (string)reader["adres"]));
                }
            }

            return klanten;
        }

        public List<Offerte> LeesOffertesVoorKlant(Klant k)
        {
            string SQL = "select * from offerte where klantnummer=@klantnummer";
            List<Offerte> offertes = new List<Offerte>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = SQL;
                cmd.Parameters.Add("@klantnummer", SqlDbType.Int);
                cmd.Parameters["@klantnummer"].Value = k.Id;
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    offertes.Add(new Offerte((int)reader["id"], (DateTime)reader["datum"], (int)reader["klantnummer"], (bool)reader["afhaal"], (bool)reader["aanleg"], (int)reader["aantal_producten"]));
                }
                return offertes;
            }
        }
    }
}
