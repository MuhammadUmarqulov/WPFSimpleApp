using ExamTask.Data.IRepositories;
using ExamTask.Data.Repositories;
using ExamTask.Domain.Constants;
using ExamTask.Domain.Entities;
using ExamTask.Service.Interfaces;
using ExamTask.Service.Models.DTOs;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ExamTask.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IStudentRepository userRepository;

        private readonly HttpClient httpClient;

        private readonly IAttachmentRepository attachmentRepository;

        private readonly string url = ApiConstants.BASE_URL + "Students/";

        public UserService()
        {
            attachmentRepository = new AttachmentRepository();
            userRepository = new StudentRepository();

            httpClient = new HttpClient();
        }

        public async Task<User> CreateAsync(StudentForCreation User)
        {
            var newUser = JsonConvert.SerializeObject(User);

            var requestContent = new StringContent
                (newUser, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync
                (url, requestContent);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var createdUser = JsonConvert.DeserializeObject<User>(content);

                await userRepository.CreateAsync(createdUser);

                return createdUser;
            }

            return null;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var response = await httpClient.DeleteAsync(url + id);

            if (response.IsSuccessStatusCode)
            {
                if (id > 320)
                {
                    await userRepository.DeleteAsync(p => p.Id == id);

                }

                return true;

            }
            return false;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue
                ("Basic", Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes("admin:12345")));

            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var resultContent = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<IEnumerable<User>>(resultContent);
            }

            return null;

        }

        public async Task<User> GetAsync(long id)
        {
            var response = await httpClient.GetAsync(url + id);

            if (response.IsSuccessStatusCode)
            {
                var resultContent = await response.Content.ReadAsStringAsync();

                resultContent = resultContent.Replace('\\', '/');

                return JsonConvert.DeserializeObject<User>(resultContent);
            }

            return null;
        }

        public async Task<User> UpdateAsync(long id, StudentForCreation User)
        {
            var newUserAsJson = JsonConvert.SerializeObject(User);

            StringContent responseContent = new(newUserAsJson,
                Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync
                            (url + id, responseContent);


            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                content = content.Replace('\\', '/');

                var updatedUser = JsonConvert.DeserializeObject<User>(content);

                userRepository.Update(updatedUser);

                return updatedUser;
            }

            return null;
        }

        public async Task UploadPicturesAsync(long id, string imagePath, string passportPath)
        {
            using HttpClient client = new();

            MultipartFormDataContent formData = new();

            if (imagePath is not null)
            {
                formData.Add(new StreamContent(File.OpenRead(imagePath)), "image", "image.png");
            }
            if (passportPath is not null)
            {
                formData.Add(new StreamContent(File.OpenRead(passportPath)), "passport", "passport.png");
            }

            var isUploadedSucceccfully = await client.PostAsync(url + "attachments/" + id, formData);



            if (isUploadedSucceccfully.IsSuccessStatusCode)
            {
                var response = await GetAsync(id);

                await attachmentRepository.CreateAsync(response.Image);

                await attachmentRepository.CreateAsync(response.Passport);

                return;
            }

        }
    }
}
