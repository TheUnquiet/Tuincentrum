﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Interfaces;
using BL.Models;
using BL.Exceptions;

namespace DL_Data
{
    public class TuincentrumRepository : ITuincentrumRepository
    {
        private string connectionString;

        public TuincentrumRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool HeeftKlant(Klant klant)
        {
            string SQL = "select count(*) from klant where id=@id and naam=@name and adres=@adres";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = SQL;
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@adres", SqlDbType.NVarChar));
                    cmd.Parameters["@id"].Value = klant.Id;
                    cmd.Parameters["@name"].Value = klant.Name;
                    cmd.Parameters["@adres"].Value = klant.Adres;

                    int count = (int)cmd.ExecuteScalar();
                    if (count > 0) return true; return false;
                } catch (Exception ex) { throw new DomeinException($"HeeftKlant -{ex.Message} "); }
            }
        }

        public void SchrijfKlant(Klant klant)
        {
            string SQL = "insert into klant(id, naam, adres) values(@id, @name, @adres";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = SQL;
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@adres", SqlDbType.NVarChar));
                    cmd.Parameters["@id"].Value = klant.Id;
                    cmd.Parameters["@name"].Value = klant.Name;
                    cmd.Parameters["@adres"].Value = klant.Adres;
                    cmd.ExecuteNonQuery();
                } catch (Exception ex) { throw new DomeinException($"SchijfKlant -{ex.Message} "); }
            }
        }
    }
}
