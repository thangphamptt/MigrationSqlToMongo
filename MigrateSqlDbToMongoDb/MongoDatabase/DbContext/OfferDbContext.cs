using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Linq;

namespace MongoDatabase.DbContext
{
	public class OfferDbContext
    {
		private readonly IMongoDatabase _database;

		public OfferDbContext(IConfiguration configuration)
		{
			var client = new MongoClient(configuration.GetSection("MongoDB:ConnectionString").Value);
			_database = client.GetDatabase(configuration.GetSection("MongoDB:OfferDatabaseName").Value);
		}

		public IMongoCollection<Domain.Offer.AggregatesModel.Offer> OfferCollection => _database.GetCollection<Domain.Offer.AggregatesModel.Offer>(nameof(Domain.Offer.AggregatesModel.Offer));
		public IQueryable<Domain.Offer.AggregatesModel.Offer> Offers => OfferCollection.AsQueryable();

		public IMongoCollection<Domain.Offer.AggregatesModel.Currency> CurrencyCollection => _database.GetCollection<Domain.Offer.AggregatesModel.Currency>(nameof(Domain.Offer.AggregatesModel.Currency));
		public IQueryable<Domain.Offer.AggregatesModel.Currency> Currencies => CurrencyCollection.AsQueryable();

		public IMongoCollection<Domain.Offer.AggregatesModel.Job> JobCollection => _database.GetCollection<Domain.Offer.AggregatesModel.Job>(nameof(Domain.Offer.AggregatesModel.Job));
		public IQueryable<Domain.Offer.AggregatesModel.Job> Jobs => JobCollection.AsQueryable();

		public IMongoCollection<Domain.Offer.AggregatesModel.User> UserCollection => _database.GetCollection<Domain.Offer.AggregatesModel.User>(nameof(Domain.Offer.AggregatesModel.User));
		public IQueryable<Domain.Offer.AggregatesModel.User> Users => UserCollection.AsQueryable();

		public IMongoCollection<Domain.Offer.AggregatesModel.Candidate> CandidateCollection => _database.GetCollection<Domain.Offer.AggregatesModel.Candidate>(nameof(Domain.Offer.AggregatesModel.Candidate));
		public IQueryable<Domain.Offer.AggregatesModel.Candidate> Candidates => CandidateCollection.AsQueryable();

		public IMongoCollection<Domain.Offer.AggregatesModel.OrganizationalUnit> OrganizationalUnitCollection => _database.GetCollection<Domain.Offer.AggregatesModel.OrganizationalUnit>(nameof(Domain.Offer.AggregatesModel.OrganizationalUnit));
		public IQueryable<Domain.Offer.AggregatesModel.OrganizationalUnit> OrganizationalUnits => OrganizationalUnitCollection.AsQueryable();

		public IMongoCollection<Domain.Offer.AggregatesModel.OfferEmailTemplate> OfferEmailTemplateCollection => _database.GetCollection<Domain.Offer.AggregatesModel.OfferEmailTemplate>(nameof(Domain.Offer.AggregatesModel.OfferEmailTemplate));
		public IQueryable<Domain.Offer.AggregatesModel.OfferEmailTemplate> OfferEmailTemplates => OfferEmailTemplateCollection.AsQueryable();
	}
}
