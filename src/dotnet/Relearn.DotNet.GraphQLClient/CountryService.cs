using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relearn.DotNet.GraphQLClient
{
    public class CountryService
    {
        GraphQLHttpClient graphQLHttpClient;
        public CountryService()
        {
            graphQLHttpClient = new GraphQLHttpClient("https://countries.trevorblades.com/",new NewtonsoftJsonSerializer());
        }
        public async Task<List<Country>> GetCountriesAsync()
        {
            var request = new GraphQLRequest
            {
                Query = @"
                query Countries {
                    countries {
                        awsRegion
                        capital
                        code
                        currencies
                        currency
                        emoji
                        emojiU
                        name
                        native
                        phone
                        phones
                    }
                }
                "
            };
            var response = await graphQLHttpClient.SendQueryAsync<CountriesResponse>(request);
            return response.Data.Countries;
        }

        public async Task<List<Country>> GetCountry(string code)
        {
            var request = new GraphQLRequest
            {
                Query = @"
                query Countries($code: String!) {
                    countries(filter: { code: { eq: $code } }) {
                        awsRegion
                        capital
                        code
                        currencies
                        currency
                        emoji
                        emojiU
                        name
                        native
                        phone
                        phones
                    }
                }
                ",
                Variables = new
                {
                    code
                }


            };
            var response = await graphQLHttpClient.SendQueryAsync<CountriesResponse>(request);
            return response.Data.Countries;
        }

        public async Task<Country> UpdateCountryCodeAsync(string code, string newCode)
        {
            var request = new GraphQLRequest
            {
                Query = @"
                mutation UpdateCountryCode($code: String!, $newCode: String!) {
                    updateCountryCode(code: $code, newCode: $newCode) {
                        awsRegion
                        capital
                        code
                        currencies
                        currency
                        emoji
                        emojiU
                        name
                        native
                        phone
                        phones
                    }
                }
                ",
                Variables = new
                {
                    code,
                    newCode
                }
            };
            var response = await graphQLHttpClient.SendQueryAsync<CountriesResponse>(request);
            return response.Data.Countries.FirstOrDefault();
        }
    }

    public class CountriesResponse
    {
        public List<Country> Countries { get; set; }
    }
    public class Country
    {
        public string AwsRegion { get; set; }
        public string Capital { get; set; }
        public string Code { get; set; }
        public List<string> Currencies { get; set; }
        public string Currency { get; set; }
        public string Emoji { get; set; }
        public string EmojiU { get; set; }
        public string Name { get; set; }
        public string Native { get; set; }
        public string Phone { get; set; }
        public List<string> Phones { get; set; }
    }

}
