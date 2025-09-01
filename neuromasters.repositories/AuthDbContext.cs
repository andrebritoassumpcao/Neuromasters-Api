using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using neuromasters.borders.Entities;

namespace neuromasters.repositories;

public class AuthDbContext(DbContextOptions options) : IdentityDbContext<User>(options);
