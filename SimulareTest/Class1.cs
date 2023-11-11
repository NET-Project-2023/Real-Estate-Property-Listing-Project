// See https://aka.ms/new-console-template for more information
using Infrastructure.Repositories;
using Infrastructure;
using System.Reflection.Metadata;
using Real_estate.Domain.Entities;

Console.WriteLine("Hello, World!");
var category = User.Create("Dorinel", "DorinMunteanu@yahoo.com", "EuSuntDorin123", Role.Customer);

var context = new RealEstateContext();
var userRepository = new UseryRepository(context);
var result = await userRepository.AddAsync(category.Value);
Console.WriteLine(result.Value.Name);