// See https://aka.ms/new-console-template for more information
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Relearn.DotNet.GraphQLClient;
using System.Runtime.InteropServices;

Console.WriteLine("Hello, World!");


CountryService countryService = new CountryService();
var countries = await countryService.GetCountry("IN");


Console.Read();


