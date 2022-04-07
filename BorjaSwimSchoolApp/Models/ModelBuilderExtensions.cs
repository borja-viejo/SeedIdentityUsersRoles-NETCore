using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BorjaSwimSchoolApp.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // Crear ROLES
            List<IdentityRole> roles = new List<IdentityRole>() {
                new IdentityRole { Name = "Administrator", NormalizedName = "ADMINISTRATOR" },
                new IdentityRole { Name = "Coach", NormalizedName = "COACH" },
                new IdentityRole { Name = "Swimmer", NormalizedName = "SWIMMER" },
                new IdentityRole { Name = "Visitor", NormalizedName = "VISITOR" }
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);

            // Crear USERS
            List<ApplicationUser> users = new List<ApplicationUser>() {
                new ApplicationUser {
                    UserName = "admin1@tajamar365.com",
                    NormalizedUserName = "ADMIN1@TAJAMAR365.COM",
                    Email = "admin1@tajamar365.com",
                    NormalizedEmail = "ADMIN1@TAJAMAR365.COM"
                },
                new ApplicationUser {
                    UserName = "coach1@tajamar365.com",
                    NormalizedUserName = "COACH1@TAJAMAR365.COM",
                    Email = "coach1@tajamar365.com",
                    NormalizedEmail = "COACH1@TAJAMAR365.COM"
                },
                new ApplicationUser {
                    UserName = "swimmer1@tajamar365.com",
                    NormalizedUserName = "SWIMMER1@TAJAMAR365.COM",
                    Email = "swimmer1@tajamar365.com",
                    NormalizedEmail = "SWIMMER1@TAJAMAR365.COM"
                },
                new ApplicationUser {
                    UserName = "visitor1@tajamar365.com",
                    NormalizedUserName = "VISITOR1@TAJAMAR365.COM",
                    Email = "visitor1@tajamar365.com",
                    NormalizedEmail = "VISITOR1@TAJAMAR365.COM"
                }
            };
            modelBuilder.Entity<ApplicationUser>().HasData(users);

            // Agregar contraseña a los usuarios
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            users[0].PasswordHash = passwordHasher.HashPassword(users[0], "$Tajamar00");
            users[1].PasswordHash = passwordHasher.HashPassword(users[1], "$Tajamar00");
            users[2].PasswordHash = passwordHasher.HashPassword(users[2], "$Tajamar00");
            users[3].PasswordHash = passwordHasher.HashPassword(users[3], "$Tajamar00");

            // Agregar roles a usuario
            List<IdentityUserRole<string>> userRoles = new List<IdentityUserRole<string>>();
            userRoles.Add(new IdentityUserRole<string> {
                UserId = users[0].Id,
                RoleId = roles.First(q => q.Name == "Administrator").Id
            });
            userRoles.Add(new IdentityUserRole<string> {
                UserId = users[1].Id,
                RoleId = roles.First(q => q.Name == "Coach").Id
            });
            userRoles.Add(new IdentityUserRole<string> {
                UserId = users[2].Id,
                RoleId = roles.First(q => q.Name == "Swimmer").Id
            });
            userRoles.Add(new IdentityUserRole<string> {
                UserId = users[3].Id,
                RoleId = roles.First(q => q.Name == "Visitor").Id
            });
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(userRoles);

            // Crear registro tabla Entrenadores
            List<Entrenador> entrenadores = new List<Entrenador>() {
                new Entrenador() {
                    EntrenadorId = 1,
                    Nombre = "Marta",
                    Apellido="Fernández",
                    Telefono = "687654321",
                    UserId = users[1].Id
                }
            };
            modelBuilder.Entity<Entrenador>().HasData(entrenadores);

            // Crear registro tabla Alumnos
            List<Alumno> alumnos = new List<Alumno>() {
                new Alumno() {
                    AlumnoId = 1,
                    Nombre = "Rosa",
                    Apellido="Flores",
                    Telefono = "612345678",
                    Sexo=(Sexo)1,
                    FxNacimiento = new System.DateTime(1995 , 06 , 15),
                    UserId = users[2].Id
                }
            };
            modelBuilder.Entity<Alumno>().HasData(alumnos);
        }
    }
}
