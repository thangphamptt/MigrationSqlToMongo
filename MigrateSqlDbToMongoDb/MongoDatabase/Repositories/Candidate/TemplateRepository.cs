using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq;
using MongoDatabase.DbContext;
using Microsoft.Extensions.Configuration;
using MongoDatabase.Domain.Candidate.AggregatesModel;

namespace Candidate.Persistance.Repositories
{
	public class TemplateRepository
    {
        private readonly CandidateDbContext _dbContext;

        public TemplateRepository(IConfiguration configuration)
		{
			_dbContext = new CandidateDbContext(configuration);
		}

		public async Task CreateTemplateAsync(Template template)
        {
            await _dbContext.TemplateCollection.InsertOneAsync(template);
        }

        public async Task<Template> GetTemplateByIdAsync(string templateId)
        {
            return await _dbContext.TemplateCollection.AsQueryable().FirstOrDefaultAsync(x => x.Id == templateId);
        }

        public async Task UpdateThankYouEmailTemplateAsync(ThankYouEmailTemplate template)
        {
            var filter = Builders<ThankYouEmailTemplate>.Filter.Where(x => x.Id == template.Id);
            var update = Builders<ThankYouEmailTemplate>.Update.Set(x => x.Attachments, template.Attachments)
                                                                .Set(x => x.Body, template.Body)
                                                                .Set(x => x.OrganizationalUnitId, template.OrganizationalUnitId)
                                                                .Set(x => x.IsDisabled, template.IsDisabled)
                                                                .Set(x => x.IsSystem, template.IsSystem)
                                                                .Set(x => x.Name, template.Name)
                                                                .Set(x => x.Subject, template.Subject);
                                                             
            await _dbContext.TemplateCollection.OfType<ThankYouEmailTemplate>().UpdateOneAsync(filter, update);
        }
    }
}
