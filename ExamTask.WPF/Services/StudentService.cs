using Newtonsoft.Json;
using ExamTask.Constants;
using ExamTask.Main.Contracts;
using ExamTask.Main.Models;
using ExamTask.Main.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ExamTask.Main.Services
{
    public class StudentService : IStudentService
    {
        private readonly HttpClient httpClient;

        private readonly string url = ApiConstants.BASE_URL + "Students/";

        public StudentService()
        {
            httpClient = new HttpClient();
        }

        public async Task<Student> CreateAsync(StudentForCreation student)
        {
            var newStudent = JsonConvert.SerializeObject(student);

            var requestContent = new StringContent
                (newStudent, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync
                (url, requestContent);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var createdStudent = JsonConvert.DeserializeObject<Student>(content);

                return createdStudent;
            }

            return null;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var response = await httpClient.DeleteAsync(url + id);

            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue
                ("Basic", Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes("admin:12345")));

            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var resultContent = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<IEnumerable<Student>>(resultContent);
            }

            return null;

        }

        public async Task<Student> GetAsync(long id)
        {
            var response = await httpClient.GetAsync(url + id);

            if (response.IsSuccessStatusCode)
            {
                var resultContent = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<Student>(resultContent);
            }

            return null;
        }

        public async Task<Student> UpdateAsync(long id, StudentForCreation student)
        {
            var newstudentAsJson = JsonConvert.SerializeObject(student);

            StringContent responseContent = new StringContent(newstudentAsJson, Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync(url + id, responseContent);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<Student>(content);
            }

            return null;
        }

        public async Task UploadPicturesAsync(long id, string imagePath, string passportPath)
        {
            using HttpClient client = new HttpClient();
            
            MultipartFormDataContent formData = new MultipartFormDataContent();
            if (imagePath is not null)
                formData.Add(new StreamContent(File.OpenRead(imagePath)), "image", "image.png");

            if (passportPath is not null)
                formData.Add(new StreamContent(File.OpenRead(passportPath)), "passport", "passport.png");

            HttpResponseMessage response = await client.PostAsync(url + "attachments/" + id, formData);

            string message = await response.Content.ReadAsStringAsync();
        }
    }
}
