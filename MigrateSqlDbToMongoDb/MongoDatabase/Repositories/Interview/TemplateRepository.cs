using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;
using MongoDB.Driver.Linq;
using MongoDB.Driver;
using MongoDatabase.Domain.Interview.AggregatesModel;
using System.Collections.Generic;

namespace Interview.Persistance.Repositories
{
	public class TemplateRepository
	{
		private readonly InterviewDbContext _dbContext;

		public TemplateRepository(IConfiguration configuration)
		{
			_dbContext = new InterviewDbContext(configuration);
		}

		public async Task CreateTemplateAsync(Template template)
		{
			await _dbContext.TemplateCollection.InsertOneAsync(template);
		}

		public async Task<InterviewPrepTemplate> GetInterviewPrepTemplateByInterViewTypeAsync(string interviewType, IList<string> organizationalUnitIds)
		{
			return await _dbContext
							 .TemplateCollection.OfType<InterviewPrepTemplate>()
												.AsQueryable()
												.FirstOrDefaultAsync(x => x.InterviewType.Equals(interviewType) &&
														(organizationalUnitIds.Contains(x.OrganizationalUnitId) || x.IsSystem));
		}

		public async Task<Template> GetTemplateByIdAsync(string templateId)
		{
			return await _dbContext.TemplateCollection.AsQueryable().FirstOrDefaultAsync(x => x.Id == templateId);
		}

		public async Task UpdateInterviewEmailTemplateAsync(InterviewEmailTemplate template)
		{
			var filter = Builders<InterviewEmailTemplate>.Filter.Where(x => x.Id == template.Id);
			var update = Builders<InterviewEmailTemplate>.Update.Set(x => x.Attachments, template.Attachments)
																.Set(x => x.Body, template.Body)
																.Set(x => x.OrganizationalUnitId, template.OrganizationalUnitId)
																.Set(x => x.IsDisabled, template.IsDisabled)
																.Set(x => x.IsSystem, template.IsSystem)
																.Set(x => x.Name, template.Name)
																.Set(x => x.Subject, template.Subject)
																.Set(x => x.Type, template.Type);

			await _dbContext.TemplateCollection.OfType<InterviewEmailTemplate>().UpdateOneAsync(filter, update);
		}

		public async Task UpdateInterviewPrepTemplateAsync(InterviewPrepTemplate template)
		{
			var filter = Builders<InterviewPrepTemplate>.Filter.Where(x => x.Id == template.Id);
			var update = Builders<InterviewPrepTemplate>.Update.Set(x => x.OrganizationalUnitId, template.OrganizationalUnitId)
																.Set(x => x.IsDisabled, template.IsDisabled)
																.Set(x => x.IsSystem, template.IsSystem)
																.Set(x => x.Name, template.Name)
																.Set(x => x.InterviewType, template.InterviewType)
																.Set(x => x.Description, template.Description)
																.Set(x => x.JobCategoryIds, template.JobCategoryIds);

			await _dbContext.TemplateCollection.OfType<InterviewPrepTemplate>().UpdateOneAsync(filter, update);
		}
	}
}
