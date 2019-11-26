using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.IO;

namespace OrderData
{
    class Program
    {
        static void Main(string[] args)
        {
            string apiUrl = "http://localhost:60866/api/SimpleOrderSearch/";

            Console.Write("Enter 1 to search by OrderID or 2 to search by MSA and Status: ");

            var SearchChoice = Console.ReadLine();
            if (SearchChoice != "1" && SearchChoice != "2")
            {
                var message = "You must select either 1 to search by OrderID or 2 to search by MSA and Status...";

                throw new System.ArgumentException(message);
            }

            long OrderID = -1;
            var MSA = -1;
            var Status = -1;
            string CompletionDte = DateTime.Today.ToString();

            try
            {
                if (SearchChoice == "1")
                {
                    Console.Write("Enter OrderID: ");
                    OrderID = Convert.ToInt64(Console.ReadLine());

                    //Console.Write("Enter Completion Date: ");
                    //CompletionDte = Console.ReadLine();
                }
                else
                {
                    Console.Write("Enter MSA: ");
                    MSA = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Enter Status: ");
                    Status = Convert.ToInt32(Console.ReadLine());

                    //Console.Write("Enter Completion Date: ");
                    //CompletionDte = Console.ReadLine();
                }
            }
            catch (Exception e)
            {
                throw new System.ArgumentException("The data you entered to search by is invalid.  Make sure the OrderID, MSA and Status are numbers greater than or equal to 0 and the Completion Date is an actual date...");
            }

            Console.WriteLine("\r\n");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if (SearchChoice == "1")
                {
                    var dataParams = new DataParams()
                    {
                        OrderID = OrderID,
                        MSA = -1,
                        Status = -1,
                        CompletionDte = Convert.ToDateTime(CompletionDte)
                    };

                    //var response = client.GetAsync($"{apiUrl}/api/SimpleOrderSearch");
                    var response = client.GetAsync($"{apiUrl}/api/SimpleOrderSearch_OrderIDOnly/{OrderID}");
                    response.Wait();

                    //WebRequest requestObject = WebRequest.Create($"{apiUrl}/api/SimpleOrderSearch/");
                    //requestObject.Method = "Post";

                    //requestObject.ContentType = "application/json";

                    //string results = null;
                    //using (var stream = new StreamWriter(requestObject.GetRequestStream()))
                    //{
                    //    stream.Write(dataParams);
                    //    stream.Flush();
                    //    stream.Close();

                    //    var httpResponse = (HttpWebResponse)requestObject.GetResponse();

                    //    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    //    {
                    //        results = streamReader.ReadToEnd();
                    //    }
                    //}


                    var result = response.Result;

                    if (!result.IsSuccessStatusCode)
                    {
                        Console.WriteLine(result.ReasonPhrase);
                        Console.ReadLine();
                    }
                    else
                    {
                        var readTask = result.Content;

                        //foreach (var rows in readTask)
                        //{
                        //    Console.WriteLine(rows.Name);
                        //}
                    }
                }
                else
                {
                    HttpResponseMessage response = client.GetAsync($"{apiUrl}{ MSA.ToString()}/{Status.ToString()}/{ CompletionDte.ToString()}").Result;

                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine(response.ReasonPhrase);
                        Console.ReadLine();
                    }
                    else
                    {
                        var dataResults = response.Content.ToString();
                        //}
                    }
                }
            }
        }
    }

    public class DataParams
    {
        public long OrderID { get; set; }
        public int MSA { get; set; }
        public int Status { get; set; }
        public DateTime CompletionDte { get; set; }
    }

    internal class DataModel
    {
        public long OrderID { get; set; }
        public int ShipperID { get; set; }
        public int DriverID { get; set; }
        public DateTime CompletionDte { get; set; }
        public int Status { get; set; }
        public string Code { get; set; }
        public int MSA { get; set; }
        public double Duration { get; set; }
        public int OfferType { get; set; }
    }
}
