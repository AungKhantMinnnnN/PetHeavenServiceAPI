using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using PetHeavenService.Data;
using PetHeavenService.Objects;

namespace PetHeavenService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetHeavenController : Controller
    {
        private readonly ILogger<PetHeavenController> _logger;
        private readonly PetHeavenDbContext _petHeavenDbContext = new PetHeavenDbContext();

        public PetHeavenController(ILogger<PetHeavenController> logger)
        {
            _logger = logger;
        }

        #region Functions related to pets
        [HttpGet("GetAllPetsForAdoption")]
        public List<Pet> GetAllPetsForAdoption()
        {
            List<Pet> allPets = new List<Pet>();
            allPets = _petHeavenDbContext.Pets.ToList();
            foreach(var pet in allPets)
            {
                pet.PetType = new PetType()
                {
                    PetTypeId = _petHeavenDbContext.PetTypes.Where(p => p.PetTypeId == pet.PetTypeId).FirstOrDefault().PetTypeId,
                    PetTypeName = _petHeavenDbContext.PetTypes.Where(p => p.PetTypeId == pet.PetTypeId).FirstOrDefault().PetTypeName,
                };

                pet.PetImage = new PetImage()
                {
                    PetImageId = _petHeavenDbContext.PetImages.Where(p => p.PetImageId == pet.PetImageId).FirstOrDefault().PetImageId,
                    PetImageBase64 = _petHeavenDbContext.PetImages.Where(p => p.PetImageId == pet.PetImageId).FirstOrDefault().PetImageBase64,
                };
            }
            return allPets;
        }

        [HttpPost("AddPetToDB")]
        public bool AddPet(Pet pet)
        {
            try
            {
                _petHeavenDbContext.Pets.Add(pet);
                _petHeavenDbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while trying to add pet to database." + ex.ToString());
                return false;
            }
        }
        #endregion

        #region Functions related to users
        [HttpGet("GetAllUsers")]
        public List<User> GetAllUsers()
        {
            List<User> allUsers = new List<User>();
            allUsers = _petHeavenDbContext.Users.ToList();
            return allUsers;
        }

        [HttpPost("AddUserToDB")]
        public bool AddUser(User user)
        {
            try
            {
                _petHeavenDbContext.Users.Add(user);
                _petHeavenDbContext.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                _logger.LogError("Error while trying to add user to database. " + ex.ToString());
                return false;
            }
        }
        #endregion


        #region Function to send email
        [HttpPost("SendEmail_Adopt")]
        public async Task<bool> SendEmail_Adopt(EmailData_Adopt data)
        {
            string senderEmail = "petheaven25112023@gmail.com";
            string password = "glqj dtoq ihyx naso";
            string host = "smtp.gmail.com";
            int port = 587;


            string subject = "Pet heaven adoption notification email";
            string body = $@"<html>
                                    <head></head>
                                        <body>
                                            <font style=""font-family: Consolas; font-size: 12pt;"">
                                            {data.fullName} has submitted an adoption form for {data.petId}.<br />
                                            Adopter's email address: {data.email} and mobile number: {data.phone}.<br />
                                            Adopter has requested {data.date} for an adoption appointment.
                                            </font>
                                        </body>
                                </html>";

            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(senderEmail);
            email.Subject = subject;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = body;
            email.To.Add(MailboxAddress.Parse("peth8222@gmail.com"));

            email.Body = bodyBuilder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(host, port, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(senderEmail, password);

            await smtp.SendAsync(email);
            smtp.Disconnect(true);

            return true;
        }

        [HttpPost("SendEmail_Release")]
        public async Task<bool> SendEmail_Release(EmailData_Release data)
        {
            string senderEmail = "petheaven25112023@gmail.com";
            string password = "glqj dtoq ihyx naso";
            string host = "smtp.gmail.com";
            int port = 587;


            string subject = "Pet heaven relase notification email";
            string body = $@"<html>
                                    <head></head>
                                        <body>
                                            <font style=""font-family: Consolas; font-size: 12pt;"">
                                            {data.fullName} has submitted a form to release a pet.<br />
                                            Adopter's email is {data.email} and mobile number is {data.phone}.<br />
                                            Pet type : {data.petType} <br />
                                            Pet name : {data.petName} <br />
                                            Pet Gender : {data.gender} <br />
                                            Release date : {data.date} <br />
                                            Reason for releasing - <br />
                                            {data.reason}
                                            </font>
                                        </body>
                                </html>";

            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(senderEmail);
            email.Subject = subject;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = body;
            email.To.Add(MailboxAddress.Parse("peth8222@gmail.com"));
            email.Body = bodyBuilder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(host, port, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(senderEmail, password);

            await smtp.SendAsync(email);
            smtp.Disconnect(true);

            return true;
        }

        #endregion
    }
}
