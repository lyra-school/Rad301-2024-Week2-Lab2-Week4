using Microsoft.EntityFrameworkCore;
using Rad301_2024_Week2_Lab2;
using Rad301_2024_Week2_Lab2.Models;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDbContext<AdDb>(opt => opt.UseInMemoryDatabase("AvailableAds"));
var connectionString = builder.Configuration.GetConnectionString("AvailableAds") ?? "Data Source=ad.db";

builder.Services.AddSqlite<AdDb>(connectionString);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

var ads = app.MapGroup("/ads");

ads.MapGet("/", async (AdDb db) =>
    await db.Ads.ToListAsync());

ads.MapGet("/{id}", async (int id, AdDb db) =>
    await db.Ads.FindAsync(id)
        is Ad ad
            ? Results.Ok(ad)
            : Results.NotFound());

ads.MapGet("/sellers/{id}", async (int id, AdDb db) =>
    await db.Sellers.FindAsync(id)
        is Seller seller
            ? Results.Ok(seller)
            : Results.NotFound());

ads.MapGet("/sellers/collection/{id}", async (int id, AdDb db) =>
{
    var query = from ad in db.Ads
                where ad.SellerId == id
                select ad;
    return query.ToList();
});

ads.MapGet("/categories/collection/{id}", async (int id, AdDb db) =>
{
    var query = from ad in db.Ads
                where ad.CategoryId == id
                orderby ad.Description ascending
                select ad;
    return query.ToList();
});

ads.MapGet("/categories/{id}", async (int id, AdDb db) =>
    await db.Categories.FindAsync(id)
        is Category category
            ? Results.Ok(category)
            : Results.NotFound());

ads.MapPost("/sellers", async (Seller seller, AdDb db) =>
{
    db.Sellers.Add(seller);
    await db.SaveChangesAsync();

    return Results.Created($"/sellers/{seller.Id}", seller);
});

ads.MapPost("/categories", async (Category cat, AdDb db) =>
{
    db.Categories.Add(cat);
    await db.SaveChangesAsync();

    return Results.Created($"/sellers/{cat.Id}", cat);
});

ads.MapPost("/", async (Ad ad, AdDb db) =>
{
    db.Ads.Add(ad);
    await db.SaveChangesAsync();
    var currentAd = await db.Ads.FindAsync(ad.Id);

    var seller =  await db.Sellers.FindAsync(ad.SellerId);

    seller.Ads.Add(currentAd);

    var category = await db.Categories.FindAsync(ad.CategoryId);
    category.Ads.Add(currentAd);

    return Results.Created($"/sellers/{ad.Id}", ad);
});

ads.MapPut("/{id}", async (int id, Ad ad, AdDb db) =>
{
    var foundAd = await db.Ads.FindAsync(id);

    if (foundAd is null) return Results.NotFound();

    foundAd.Description = ad.Description;
    foundAd.Price = ad.Price;
    foundAd.SellerId = ad.SellerId;
    foundAd.CategoryId = ad.CategoryId;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

ads.MapDelete("/{id}", async (int id, AdDb db) =>
{
    if (await db.Ads.FindAsync(id) is Ad ad)
    {
        db.Ads.Remove(ad);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});

app.Run();