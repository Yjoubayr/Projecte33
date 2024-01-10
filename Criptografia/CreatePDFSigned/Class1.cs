using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DAMSecurityLib;
using DAMSecurityLib.Crypto;

namespace CrearPDFSignat
{
    class Class1
    {
        static void CrearPDFSignat(){
            Console.WriteLine("Introduce la URL de la api para adquirir la lista: ");
            String url = Console.ReadLine();
            try
            {
                // Fer la peticio get del json per despres ficarlo en el pdf i signarlo amb la llibreria importada
                Task<List<string>> json = PeticioGET(url);
                string destUrl = "C:\\Users\\user\\Documents\\CrearPDFSignat";
                Sign.CreateBlancPDF(destUrl);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        //TODO: Fer que es fagi correctament la peticioGET
        static async Task<List<string>> PeticioGET(string sURL)
        {
            List<string> arrayList = new List<string>();
            using (HttpClient client = new HttpClient())
            {

                try
                {
                    // Realizar la solicitud GET
                    HttpResponseMessage response = await client.GetAsync(sURL);
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(content);
                        // Pasem l'objecte a string para enviar-lo por sock


                        return arrayList;
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode}");
                        arrayList.Add(response.StatusCode.ToString());
                        return arrayList;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return arrayList;
                }

            }

        }
    }
}
