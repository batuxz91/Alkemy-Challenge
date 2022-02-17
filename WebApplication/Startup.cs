using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Linq;
using WebApplication.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using Microsoft.AspNetCore.Identity;

namespace WebApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApplication", Version = "v1" });
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => 
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "webapplication.com",
                ValidAudience = "webapplication.com",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Secret_Key"])),
                ClockSkew = TimeSpan.Zero
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseAuthentication();

            GenerarDatos(context);

        }

        void GenerarDatos(ApplicationDbContext context)
        {
            string genero1 = "Action";
            string imgGenero1 = "images/generos/action.png";
            string genero2 = "Adventure";
            string imgGenero2 = "images/generos/adventure.png";
            string genero3 = "Comedy";
            string imgGenero3 = "images/generos/comedy.png";
            string genero4 = "Mystery";
            string imgGenero4 = "images/generos/mystery.png";
            string genero5 = "Fantasy";
            string imgGenero5 = "images/generos/fantasy.png";

            string personaje1 = "Mickey Mouse";
            string imgPersonaje1 = "images/personajes/mickeymouse.png";
            string historiapersonaje1 = "";
            string personaje2 = "Pato Donal";
            string imgPersonaje2 = "images/personajes/patodonal.png";
            string historiapersonaje2 = "";
            string personaje3 = "Goofy";
            string imgPersonaje3 = "images/personajes/goofy.png";
            string historiapersonaje3 = "";
            string personaje4 = "Ariel";
            string imgPersonaje4 = "images/personajes/ariel.png";
            string historiapersonaje4 = "";
            string personaje5 = "Blancanieves";
            string imgPersonaje5 = "images/personajes/blancanieves.png";
            string historiapersonaje5 = "";

            string pelicula1 = "Mickey, Donald, Goofy: Los tres mosqueteros";
            string imgPelicula1 = "images/peliculas/lostresmosqueteros.png";
            string pelicula2 = "El Club de los Villanos con Mickey y sus Amigos";
            string imgPelicula2 = "images/peliculas/elclubdelosvillanos.png";
            string pelicula3 = "Fantasía 2000";
            string imgPelicula3 = "images/peliculas/fantasia2000.png";
            string pelicula4 = "La Sirenita";
            string imgPelicula4 = "images/peliculas/lasirenita.png";
            string pelicula5 = "Blancanieves";
            string imgPelicula5 = "images/peliculas/blancanieves.png";

            if (context.Personaje.Any())
            {
                return;
            }

            var generos = new Genero[]
               {
                    new Genero() {Nombre = genero1, Imagen = imgGenero1},
                    new Genero() {Nombre = genero2, Imagen = imgGenero2},
                    new Genero() {Nombre = genero3, Imagen = imgGenero3},
                    new Genero() {Nombre = genero4, Imagen = imgGenero4},
                    new Genero() {Nombre = genero5, Imagen = imgGenero5}
               };

            foreach (Genero g in generos)
            {
                context.Genero.Add(g);
            }

            context.SaveChanges();

            var personajes = new Personaje[]
            {
                    new Personaje() {Nombre = personaje1, 
                                     Imagen = imgPersonaje1, 
                                     Edad = 99, 
                                     Peso=30.0f, 
                                     Historia= historiapersonaje1},

                    new Personaje() {Nombre = personaje2,
                                     Imagen = imgPersonaje2, 
                                     Edad = 80, 
                                     Peso=40.0f, 
                                     Historia= historiapersonaje2},

                    new Personaje() {Nombre = personaje3, 
                                     Imagen = imgPersonaje3, 
                                     Edad = 99, 
                                     Peso=60.0f, 
                                     Historia= historiapersonaje3},

                    new Personaje() {Nombre = personaje4,
                                     Imagen = imgPersonaje4, 
                                     Edad = 20, 
                                     Peso=50.0f, 
                                     Historia=historiapersonaje4},

                    new Personaje() {Nombre = personaje5,
                                     Imagen = imgPersonaje5,
                                     Edad = 20,
                                     Peso=50.0f,
                                     Historia=historiapersonaje5}
            };

            foreach (Personaje p in personajes)
            {
                context.Personaje.Add(p);
            }
            context.SaveChanges();


            var peliculas = new Pelicula[]
            {
                    

                    new Pelicula() {Titulo = pelicula1, 
                                    Imagen = imgPelicula1, 
                                    FechaCreacion = DateTime.Parse("2017-08-17"), 
                                    Calificacion=4, 
                                    GeneroId = generos.Single( s => s.Nombre == genero2).Id},

                    new Pelicula() {Titulo = pelicula2,
                                    Imagen = imgPelicula2, 
                                    FechaCreacion = DateTime.Parse("2001-11-01"), 
                                    Calificacion=2, 
                                    GeneroId = generos.Single( s => s.Nombre == genero3).Id},

                    new Pelicula() {Titulo = pelicula3,
                                    Imagen = imgPelicula3, 
                                    FechaCreacion = DateTime.Parse("1999-12-17"), 
                                    Calificacion=3, 
                                    GeneroId = generos.Single( s => s.Nombre == genero5).Id},

                    new Pelicula() {Titulo = pelicula4,
                                    Imagen = imgPelicula4,
                                    FechaCreacion = DateTime.Parse("1989-01-01"),
                                    Calificacion=5,
                                    GeneroId = generos.Single( s => s.Nombre == genero2).Id},

                    new Pelicula() {Titulo = pelicula5,
                                    Imagen = imgPelicula5,
                                    FechaCreacion = DateTime.Parse("1989-01-01"),
                                    Calificacion=5,
                                    GeneroId = generos.Single( s => s.Nombre == genero5).Id}

            };

            foreach (Pelicula p in peliculas)
            {
                context.Pelicula.Add(p);
            }
            context.SaveChanges();

            var peliculasxpersonajes = new PeliculaPersonaje[]
            {
                    

                    new PeliculaPersonaje() {PeliculaId = peliculas.Single(s => s.Titulo==pelicula1).Id, 
                                             PersonajeId = personajes.Single(s => s.Nombre==personaje1).Id},
                    new PeliculaPersonaje() {PeliculaId = peliculas.Single(s => s.Titulo==pelicula1).Id, 
                                             PersonajeId = personajes.Single(s => s.Nombre==personaje2).Id },
                    new PeliculaPersonaje() {PeliculaId = peliculas.Single(s => s.Titulo==pelicula1).Id, 
                                             PersonajeId = personajes.Single(s => s.Nombre==personaje3).Id },

                    new PeliculaPersonaje() {PeliculaId = peliculas.Single(s => s.Titulo==pelicula2).Id, 
                                             PersonajeId = personajes.Single(s => s.Nombre==personaje1).Id},
                    new PeliculaPersonaje() {PeliculaId = peliculas.Single(s => s.Titulo==pelicula2).Id, 
                                             PersonajeId = personajes.Single(s => s.Nombre==personaje2).Id },
                    new PeliculaPersonaje() {PeliculaId = peliculas.Single(s => s.Titulo==pelicula2).Id, 
                                             PersonajeId = personajes.Single(s => s.Nombre==personaje3).Id },
                    
                    new PeliculaPersonaje() {PeliculaId = peliculas.Single(s => s.Titulo==pelicula3).Id, 
                                             PersonajeId = personajes.Single(s => s.Nombre==personaje1).Id },
                    new PeliculaPersonaje() {PeliculaId = peliculas.Single(s => s.Titulo==pelicula3).Id, 
                                             PersonajeId = personajes.Single(s => s.Nombre==personaje2).Id },
                    new PeliculaPersonaje() {PeliculaId = peliculas.Single(s => s.Titulo==pelicula3).Id, 
                                             PersonajeId = personajes.Single(s => s.Nombre==personaje3).Id },

                    new PeliculaPersonaje() {PeliculaId = peliculas.Single(s => s.Titulo==pelicula4).Id, 
                                             PersonajeId = personajes.Single(s => s.Nombre==personaje4).Id },

                    new PeliculaPersonaje() {PeliculaId = peliculas.Single(s => s.Titulo==pelicula5).Id,
                                             PersonajeId = personajes.Single(s => s.Nombre==personaje5).Id },
            };

            foreach (PeliculaPersonaje e in peliculasxpersonajes)
            {
                var peliculaPersonajeInDataBase = context.PeliculaPersonajes.Where(
                    s =>
                            s.Pelicula.Id == e.PeliculaId &&
                            s.Personaje.Id == e.PersonajeId).SingleOrDefault();
                if (peliculaPersonajeInDataBase == null)
                {
                    context.PeliculaPersonajes.Add(e);
                }
            }
            context.SaveChanges();
        }

    }
}
