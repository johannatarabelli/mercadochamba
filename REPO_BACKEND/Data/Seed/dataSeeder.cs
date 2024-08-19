using backnc.Common;
using backnc.Data.Context;
using backnc.Data.POCOEntities;
using Microsoft.EntityFrameworkCore;

public class DataSeeder
{
	private readonly AppDbContext _context;

	public DataSeeder(AppDbContext context)
	{
		_context = context;
	}

	public async Task SeedAsync()
	{
		await CreateRolesAsync();
		await CreateAdminUserAsync();
		await CreateClienteAsync();
		await CreateCountriesAsync();
		await CreateCategoriesAsync();

	}

	private async Task CreateRolesAsync()
	{
		if (await _context.Roles.AnyAsync()) return;

		var roles = new[]
		{
			new Role { Name = "Admin" },
			new Role { Name = "Cliente" }
		};

		await _context.Roles.AddRangeAsync(roles);
		await _context.SaveChangesAsync();
	}

	private async Task CreateCategoriesAsync()
	{
		if (await _context.Categories.AnyAsync()) return;

		var categories = new[]
		{
		new Category { Name = "Programador" },
		new Category { Name = "Electricista" },
		new Category { Name = "Plomero" },
		new Category { Name = "Odontólogo" },
		new Category { Name = "Contador" },
		new Category { Name = "Abogado" },
		new Category { Name = "Profesor" },
		new Category { Name = "Albañil" },
		new Category { Name = "Mecánico" }, 
        new Category { Name = "Carpintero" } 
    };

		await _context.Categories.AddRangeAsync(categories);
		await _context.SaveChangesAsync();
	}

	private async Task CreateAdminUserAsync()
	{
		if (await _context.Users.AnyAsync(u => u.UserName == "admin")) return;

		var adminRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Admin");
		if (adminRole == null) throw new Exception("Role 'Admin' does not exist.");

		var newUser = new User
		{
			UserName = "admin",
			firstName = "Administrador",
			lastName = "Administrador",
			email = "admin@gmail.com",
			Password = PasswordHasher.HashPassword("Admin123!")
		};

		_context.Users.Add(newUser);
		await _context.SaveChangesAsync();

		var userRole = new UserRole
		{
			UserId = newUser.Id,
			RoleId = adminRole.Id
		};

		_context.UserRoles.Add(userRole);
		await _context.SaveChangesAsync();
	}
	private async Task CreateClienteAsync()
	{
		if (await _context.Users.AnyAsync(u => u.UserName == "cliente")) return;

		var adminRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Cliente");
		if (adminRole == null) throw new Exception("Role 'Cliente' does not exist.");

		var newUser = new User
		{
			UserName = "cliente",
			firstName = "cliente",
			lastName = "cliente",
			email = "cliente@gmail.com",
			Password = PasswordHasher.HashPassword("Cliente123!")
		};

		_context.Users.Add(newUser);
		await _context.SaveChangesAsync();

		var userRole = new UserRole
		{
			UserId = newUser.Id,
			RoleId = adminRole.Id
		};

		_context.UserRoles.Add(userRole);
		await _context.SaveChangesAsync();
	}
	private async Task CreateCountriesAsync()
	{
		if (await _context.Countries.AnyAsync()) return;

		var countries = new[]
		{
		new Country
		{
			Name = "Argentina",
			Provinces = new List<Province>
			{
				new Province { Name = "Cordoba", Neighborhoods = CreateNeighborhoods("Cordoba") },
				new Province { Name = "Buenos Aires", Neighborhoods = CreateNeighborhoods("Buenos Aires") },
				new Province { Name = "CABA", Neighborhoods = CreateNeighborhoods("CABA") },
				new Province { Name = "Mendoza", Neighborhoods = CreateNeighborhoods("Mendoza") },
				new Province { Name = "San Juan", Neighborhoods = CreateNeighborhoods("San Juan") },
				new Province { Name = "Salta", Neighborhoods = CreateNeighborhoods("Salta") }
			}
		},
		new Country
		{
			Name = "Brasil",
			Provinces = new List<Province>
			{
				new Province { Name = "São Paulo", Neighborhoods = CreateNeighborhoods("São Paulo") },
				new Province { Name = "Rio de Janeiro", Neighborhoods = CreateNeighborhoods("Rio de Janeiro") },
				new Province { Name = "Minas Gerais", Neighborhoods = CreateNeighborhoods("Minas Gerais") },
				new Province { Name = "Bahia", Neighborhoods = CreateNeighborhoods("Bahia") },
				new Province { Name = "Paraná", Neighborhoods = CreateNeighborhoods("Paraná") }
			}
		},
        // Agrega los demás países de manera similar
        new Country
		{
			Name = "Perú",
			Provinces = new List<Province>
			{
				new Province { Name = "Lima", Neighborhoods = CreateNeighborhoods("Lima") },
				new Province { Name = "Arequipa", Neighborhoods = CreateNeighborhoods("Arequipa") },
				new Province { Name = "Trujillo", Neighborhoods = CreateNeighborhoods("Trujillo") },
				new Province { Name = "Cusco", Neighborhoods = CreateNeighborhoods("Cusco") },
				new Province { Name = "Piura", Neighborhoods = CreateNeighborhoods("Piura") }
			}
		},
		new Country
		{
			Name = "Ecuador",
			Provinces = new List<Province>
			{
				new Province { Name = "Quito", Neighborhoods = CreateNeighborhoods("Quito") },
				new Province { Name = "Guayaquil", Neighborhoods = CreateNeighborhoods("Guayaquil") },
				new Province { Name = "Cuenca", Neighborhoods = CreateNeighborhoods("Cuenca") },
				new Province { Name = "Manta", Neighborhoods = CreateNeighborhoods("Manta") },
				new Province { Name = "Ambato", Neighborhoods = CreateNeighborhoods("Ambato") }
			}
		},
		new Country
		{
			Name = "Chile",
			Provinces = new List<Province>
			{
				new Province { Name = "Santiago", Neighborhoods = CreateNeighborhoods("Santiago") },
				new Province { Name = "Valparaíso", Neighborhoods = CreateNeighborhoods("Valparaíso") },
				new Province { Name = "Concepción", Neighborhoods = CreateNeighborhoods("Concepción") },
				new Province { Name = "La Serena", Neighborhoods = CreateNeighborhoods("La Serena") },
				new Province { Name = "Antofagasta", Neighborhoods = CreateNeighborhoods("Antofagasta") }
			}
		},
		new Country
		{
			Name = "Uruguay",
			Provinces = new List<Province>
			{
				new Province { Name = "Montevideo", Neighborhoods = CreateNeighborhoods("Montevideo") },
				new Province { Name = "Punta del Este", Neighborhoods = CreateNeighborhoods("Punta del Este") },
				new Province { Name = "Salto", Neighborhoods = CreateNeighborhoods("Salto") },
				new Province { Name = "Paysandú", Neighborhoods = CreateNeighborhoods("Paysandú") },
				new Province { Name = "Rivera", Neighborhoods = CreateNeighborhoods("Rivera") }
			}
		},
		new Country
		{
			Name = "Colombia",
			Provinces = new List<Province>
			{
				new Province { Name = "Bogotá", Neighborhoods = CreateNeighborhoods("Bogotá") },
				new Province { Name = "Medellín", Neighborhoods = CreateNeighborhoods("Medellín") },
				new Province { Name = "Cali", Neighborhoods = CreateNeighborhoods("Cali") },
				new Province { Name = "Barranquilla", Neighborhoods = CreateNeighborhoods("Barranquilla") },
				new Province { Name = "Cartagena", Neighborhoods = CreateNeighborhoods("Cartagena") }
			}
		},
		new Country
		{
			Name = "Venezuela",
			Provinces = new List<Province>
			{
				new Province { Name = "Caracas", Neighborhoods = CreateNeighborhoods("Caracas") },
				new Province { Name = "Maracaibo", Neighborhoods = CreateNeighborhoods("Maracaibo") },
				new Province { Name = "Valencia", Neighborhoods = CreateNeighborhoods("Valencia") },
				new Province { Name = "Barquisimeto", Neighborhoods = CreateNeighborhoods("Barquisimeto") },
				new Province { Name = "Puerto La Cruz", Neighborhoods = CreateNeighborhoods("Puerto La Cruz") }
			}
		}
	};

		await _context.Countries.AddRangeAsync(countries);
		await _context.SaveChangesAsync();
	}

	private List<Neighborhood> CreateNeighborhoods(string provinceName)
	{
		var neighborhoods = new Dictionary<string, List<string>>
	{

        // Barrios conocidos en Buenos Aires
        { "Buenos Aires", new List<string> { "Palermo", "Recoleta", "San Telmo", "La Boca", "Belgrano" } },

		//Barrios de córdoba
		{ "Cordoba", new List<string> { "Centro", "Pueyrredon", "General Paz", "Barrio Talleres", "Yofre sur" } },

        // Barrios conocidos en CABA
        { "CABA", new List<string> { "Palermo", "Recoleta", "San Telmo", "La Boca", "Belgrano" } },

        // Barrios conocidos en Mendoza
        { "Mendoza", new List<string> { "Ciudad", "Godoy Cruz", "Guaymallén", "Maipú", "Luján de Cuyo" } },

        // Barrios conocidos en San Juan
        { "San Juan", new List<string> { "Centro", "Rivadavia", "Rawson", "Chimbas", "Santa Lucía" } },

        // Barrios conocidos en Salta
        { "Salta", new List<string> { "Centro", "Caballito", "Bº Norte", "Bº Sur", "San Lorenzo" } },

        // Barrios conocidos en São Paulo
        { "São Paulo", new List<string> { "Paulista", "Vila Madalena", "Liberdade", "Pinheiros", "Itaim Bibi" } },

        // Barrios conocidos en Rio de Janeiro
        { "Rio de Janeiro", new List<string> { "Copacabana", "Ipanema", "Lapa", "Santa Teresa", "Leblon" } },

        // Barrios conocidos en Minas Gerais
        { "Minas Gerais", new List<string> { "Savassi", "Centro", "Pampulha", "São Bento", "Santa Efigênia" } },

        // Barrios conocidos en Bahia
        { "Bahia", new List<string> { "Pelourinho", "Barra", "Ondina", "Campo Grande", "Caminho das Árvores" } },

        // Barrios conocidos en Paraná
        { "Paraná", new List<string> { "Centro", "Batel", "Bigorrilho", "Água Verde", "Santa Felicidade" } },

        // Barrios conocidos en Lima
        { "Lima", new List<string> { "Miraflores", "San Isidro", "Barranco", "Surco", "Centro Histórico" } },

        // Barrios conocidos en Arequipa
        { "Arequipa", new List<string> { "Centro", "Yanahuara", "Cayma", "Socabaya", "Miraflores" } },

        // Barrios conocidos en Trujillo
        { "Trujillo", new List<string> { "Centro", "La Esperanza", "El Porvenir", "Víctor Larco", "Moche" } },

        // Barrios conocidos en Cusco
        { "Cusco", new List<string> { "Centro Histórico", "San Blas", "Santo Domingo", "Wanchaq", "Tambo Machay" } },

        // Barrios conocidos en Piura
        { "Piura", new List<string> { "Centro", "Castilla", "Benavides", "Salitral", "Piura Viejo" } },

        // Barrios conocidos en Quito
        { "Quito", new List<string> { "La Carolina", "Centro Histórico", "La Floresta", "Cumbayá", "Bellavista" } },

        // Barrios conocidos en Guayaquil
        { "Guayaquil", new List<string> { "Centro", "Urdesa", "Samborondón", "La Alborada", "Los Ceibos" } },

        // Barrios conocidos en Cuenca
        { "Cuenca", new List<string> { "Centro Histórico", "El Vergel", "La Cazuela", "Barranco", "San Sebastián" } },

        // Barrios conocidos en Manta
        { "Manta", new List<string> { "Centro", "Umina", "Tarqui", "Eloy Alfaro", "Los Esteros" } },

        // Barrios conocidos en Ambato
        { "Ambato", new List<string> { "Centro", "Ficoa", "Huachi", "La Península", "Ambato Viejo" } },

        // Barrios conocidos en Santiago
        { "Santiago", new List<string> { "Providencia", "Las Condes", "Ñuñoa", "Bella Vista", "El Golf" } },

        // Barrios conocidos en Valparaíso
        { "Valparaíso", new List<string> { "Centro", "Cervecería", "Playa Ancha", "El Almendral", "Mirasol" } },

        // Barrios conocidos en Concepción
        { "Concepción", new List<string> { "Centro", "San Pedro", "Chillán", "Penco", "Hualpén" } },

        // Barrios conocidos en La Serena
        { "La Serena", new List<string> { "Centro", "El Faro", "La Florida", "Las Compañías", "Villa El Valle" } },

        // Barrios conocidos en Antofagasta
        { "Antofagasta", new List<string> { "Centro", "El Golf", "Los Arenales", "Norte", "Sur" } },

        // Barrios conocidos en Montevideo
        { "Montevideo", new List<string> { "Ciudad Vieja", "Pocitos", "Carrasco", "Tres Cruces", "Palermo" } },

        // Barrios conocidos en Punta del Este
        { "Punta del Este", new List<string> { "La Barra", "Punta Ballena", "José Ignacio", "Beverly Hills", "El Chorro" } },

        // Barrios conocidos en Salto
        { "Salto", new List<string> { "Centro", "Salto Nuevo", "Salto Grande", "Salto Chico", "Parque Solari" } },

        // Barrios conocidos en Paysandú
        { "Paysandú", new List<string> { "Centro", "Paysandú Nuevo", "Lago de la Plata", "Quebracho", "Colón" } },

        // Barrios conocidos en Rivera
        { "Rivera", new List<string> { "Centro", "Artigas", "Lavalleja", "Santana", "Río Branco" } },

        // Barrios conocidos en Bogotá
        { "Bogotá", new List<string> { "Chapinero", "Zona T", "La Candelaria", "Usaquén", "Suba" } },

        // Barrios conocidos en Medellín
        { "Medellín", new List<string> { "El Poblado", "Laureles", "Belen", "Centro", "Envigado" } },

        // Barrios conocidos en Cali
        { "Cali", new List<string> { "Centro", "San Fernando", "El Peñón", "Ciudad Jardín", "Granja" } },

        // Barrios conocidos en Barranquilla
        { "Barranquilla", new List<string> { "El Prado", "Riomar", "Centro", "Villa Santos", "Alto Prado" } },

        // Barrios conocidos en Cartagena
        { "Cartagena", new List<string> { "Centro Histórico", "Bocagrande", "Getsemaní", "Castillo Grande", "La Boquilla" } },

        // Barrios conocidos en Caracas
        { "Caracas", new List<string> { "Chacao", "Altamira", "Las Mercedes", "El Hatillo", "La Candelaria" } },

        // Barrios conocidos en Maracaibo
        { "Maracaibo", new List<string> { "El Saladillo", "La Lago", "Delicias", "Santa Rita", "Chiquinquirá" } },

        // Barrios conocidos en Valencia
        { "Valencia", new List<string> { "San Diego", "La Viña", "El Trigal", "La Isabelica", "Centro" } },

        // Barrios conocidos en Barquisimeto
        { "Barquisimeto", new List<string> { "Centro", "El Ujano", "El Manzano", "La Carucieña", "Joaquín Crespo" } },

        // Barrios conocidos en Puerto La Cruz
        { "Puerto La Cruz", new List<string> { "Centro", "Lechería", "Puerto Escondido", "Playa Mansa", "Marina" } },
	};

		return neighborhoods.ContainsKey(provinceName)
			? neighborhoods[provinceName].Select(name => new Neighborhood { Name = name }).ToList()
			: new List<Neighborhood>();
	}






}
