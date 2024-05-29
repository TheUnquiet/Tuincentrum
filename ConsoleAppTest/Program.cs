using BL.Exceptions;
using BL.Interfaces;
using BL.Models;
using DL_Data;
using Microsoft.IdentityModel.Protocols;
using System.Configuration;

DateTime dateTime = DateTime.Now;
var date = dateTime.Date;
Console.WriteLine(date.ToString("dd/MM/yyyy"));
try
{
    Product p = new Product("Name", " ", 10, "D");
} catch (TuincentrumException)
{
    Console.WriteLine("Error while writing...");
}