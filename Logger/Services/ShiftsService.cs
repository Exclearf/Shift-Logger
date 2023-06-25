using ShiftsLogger.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ShiftsLogger.Services
{
    public class ShiftsService
    {
        static HttpClient client = new HttpClient();

        public static async Task RunAsync()
        {
            client.BaseAddress = new Uri("https://localhost:7274");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static async Task<Uri?> CreateShiftAsync(ShiftModel shift)
        {
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync("api/shifts", shift);
            responseMessage.EnsureSuccessStatusCode();

            return responseMessage.Headers.Location;
        }
        public static async Task<ShiftModel?> GetShiftAsyncById(long id)
        {
            ShiftModel shift = null!;

            try
            {
                HttpResponseMessage httpResponseMessage = await client.GetAsync($"api/shifts/{id}");
                httpResponseMessage.EnsureSuccessStatusCode();

                shift = await httpResponseMessage.Content.ReadAsAsync<ShiftModel>();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return shift;
        }
         
        public static async Task<ShiftModel?> UpdateShiftAsync(ShiftModel? shift)
        {
            var httpResponseMessage = await client.PutAsJsonAsync<ShiftModel?>($"api/shifts/{shift?.Id}", shift);
            httpResponseMessage.EnsureSuccessStatusCode();

            shift = await httpResponseMessage.Content.ReadFromJsonAsync<ShiftModel>();
            return shift;
        }

        public static async Task<HttpStatusCode> DeleteShiftByIdAsync(long id)
        {
            HttpResponseMessage httpResponseMessage = await client.DeleteAsync($"api/shifts/{id}");

            return httpResponseMessage.StatusCode;
        }

        public static async Task<List<ShiftModel>?> GetShifts()
        {
            HttpResponseMessage httpResponseMessage = await client.GetAsync("api/shifts");
            httpResponseMessage.EnsureSuccessStatusCode();

            List<ShiftModel>? shiftModels = await httpResponseMessage.Content.ReadFromJsonAsync<List<ShiftModel>?>();

            return shiftModels;
        }

        public static void ShowShift(ShiftModel s)
        {
            Console.WriteLine($"Shift info:\nID: {s.Id}\nName: {s.Name}\nStart: {s.ShiftStart}\nEnd: {s.ShiftEnd}");
        }
    }
}
