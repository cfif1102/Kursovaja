using Microsoft.EntityFrameworkCore;

namespace Lab_4.Data
{
    public static class DbInitializer
    {
        public static void Initialize(StudentsContext db)
        {
            db.Database.EnsureCreated();

            if (db.Parents.Any())
            {
                return;
            }

            for (int i = 1; i <= 500; i++)
            {
                db.Parents.Add(new Parent
                {
                    Parent1Name = Faker.Name.FullName(),
                    Parent2Name = Faker.Name.FullName(),
                });
            }

            db.SaveChanges();

            for (int i = 1; i <= 500; i++)
            {
                db.Faculties.Add(new Faculty
                {
                    FacultyName = Faker.Company.Name()
                });
            }

            db.SaveChanges();

            for (int i = 1; i <= 10000; i++)
            {
                db.Specialties.Add(new Specialty
                {
                    SpecialtyName = Faker.Company.Name(),
                    FacultyId = Faker.RandomNumber.Next(1, 500)
                });
            }

            db.SaveChanges();

            for (int i = 1; i <= 500; i++)
            {
                db.EducationInstitutions.Add(new EducationInstitution
                {
                    InstitutionName = Faker.Company.Name()
                });
            }

            db.SaveChanges();

            for (int i = 1; i <= 10000; i++)
            {
                db.AdmissionPlans.Add(new AdmissionPlan
                {
                    Year = Faker.RandomNumber.Next(1980, 2023),
                    SpecialtyId = Faker.RandomNumber.Next(1, 500),
                    NumberOfSeats = Faker.RandomNumber.Next(10, 100)
                });
            }

            db.SaveChanges();

            for (var i = 1; i <= 500; i++)
            {
                db.AdmissionsOfficers.Add(new AdmissionsOfficer
                {
                    FullName = Faker.Name.FullName(),
                    Department = Faker.Company.BS()
                });
            }

            db.SaveChanges();

            for (var i = 1; i <= 10000; i++)
            {
                db.Applicants.Add(new Applicant
                {
                    Name = Faker.Name.First(),
                    Middlename = Faker.Name.Last(),
                    Surname = Faker.Name.Last(),
                    IdentificationDocument = Faker.Internet.DomainName(),
                    EducationDocument = Faker.Internet.UserName(),
                    AverageGrade = Faker.RandomNumber.Next(1, 10),
                    ResidentialAddress = Faker.Address.StreetAddress(),
                    Phone = Faker.Lorem.Words(1).First(),
                    Email = Faker.Internet.Email(),
                    GraduationYear = Faker.RandomNumber.Next(1980, 2020),
                    EducationInstitutionId = Faker.RandomNumber.Next(1, 500),
                    ForeignLanguage = Faker.Lorem.Words(1).First(),
                    ParentsId = Faker.RandomNumber.Next(1, 500)
                });
            }

            db.SaveChanges();

            for (var i = 1; i <= 20000; i++)
            {
                db.ApplicantCertificates.Add(new ApplicantCertificate
                {
                    ApplicantId = Faker.RandomNumber.Next(1, 10000),
                    Grade = Faker.RandomNumber.Next(1, 10)
                });
            }

            db.SaveChanges();

            for (var i = 1; i <= 10000; i++)
            {
                db.AdmissionApplications.Add(new AdmissionApplication
                {
                    ApplicantId = Faker.RandomNumber.Next(1, 10000),
                    ApplicationDate = Faker.Identification.DateOfBirth(),
                    SpecialtyId = Faker.RandomNumber.Next(1, 500),
                    AdmissionsOfficerId = Faker.RandomNumber.Next(1, 500),
                    OtherDetails = string.Join("", Faker.Lorem.Words(Faker.RandomNumber.Next(1, 25)))
                });
            }

            db.SaveChanges();
        }
    }
}
