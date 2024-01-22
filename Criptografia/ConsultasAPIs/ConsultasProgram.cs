namespace ConsultasAPIs
{
    public class ConsultasProgram
    {
        public void RequestProcess(string RequestedList)
        {
            switch(RequestedList)
            {
                case "list1":
                    list1Function();
                    break;
                case "list2":
                    list2Function();
                    break;
                case "list3":
                    list3Function();
                    break;
                case "list4":
                    list4Function();
                    break;
            }
        }
        private static async Task list1Function()
        {
            string requestURL = "https://ejemplo.com/api/datos";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(requestURL);

                    if (response.IsSuccessStatusCode)
                    {
                        // Procesa la respuesta si la solicitud fue exitosa
                        string responseData = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Respuesta exitosa: " + responseData);
                    }
                    else
                    {
                        // Maneja el caso en que la solicitud no fue exitosa
                        Console.WriteLine("Error en la solicitud. Código de estado: " + response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    // Maneja excepciones si ocurren durante la solicitud
                    Console.WriteLine("Error durante la solicitud: " + ex.Message);
                }
            }
        }

        private static async Task list2Function()
        {
            string requestURL = "https://ejemplo.com/api/datos";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(requestURL);

                    if (response.IsSuccessStatusCode)
                    {
                        // Procesa la respuesta si la solicitud fue exitosa
                        string responseData = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Respuesta exitosa: " + responseData);
                    }
                    else
                    {
                        // Maneja el caso en que la solicitud no fue exitosa
                        Console.WriteLine("Error en la solicitud. Código de estado: " + response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    // Maneja excepciones si ocurren durante la solicitud
                    Console.WriteLine("Error durante la solicitud: " + ex.Message);
                }
            }
        }

        private static async Task list3Function()
        {
            string requestURL = "https://ejemplo.com/api/datos";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(requestURL);

                    if (response.IsSuccessStatusCode)
                    {
                        // Procesa la respuesta si la solicitud fue exitosa
                        string responseData = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Respuesta exitosa: " + responseData);
                    }
                    else
                    {
                        // Maneja el caso en que la solicitud no fue exitosa
                        Console.WriteLine("Error en la solicitud. Código de estado: " + response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    // Maneja excepciones si ocurren durante la solicitud
                    Console.WriteLine("Error durante la solicitud: " + ex.Message);
                }
            }
        }

        private static async Task list4Function()
        {
            string requestURL = "https://ejemplo.com/api/datos";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(requestURL);

                    if (response.IsSuccessStatusCode)
                    {
                        // Procesa la respuesta si la solicitud fue exitosa
                        string responseData = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Respuesta exitosa: " + responseData);
                    }
                    else
                    {
                        // Maneja el caso en que la solicitud no fue exitosa
                        Console.WriteLine("Error en la solicitud. Código de estado: " + response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    // Maneja excepciones si ocurren durante la solicitud
                    Console.WriteLine("Error durante la solicitud: " + ex.Message);
                }
            }
        }
    }
}
