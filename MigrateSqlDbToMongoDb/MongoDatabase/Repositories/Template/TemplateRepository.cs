using System.Threading.Tasks;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using MongoDatabase.DbContext;
using MongoDatabase.Domain.Template.AggregatesModel;

namespace MongoDatabase.Repositories.Template
{
	public class TemplateRepository
	{
		private readonly TemplateDbContext _dbContext;

		public TemplateRepository(IConfiguration configuration)
		{
			_dbContext = new TemplateDbContext(configuration);
		}

		public async Task<Domain.Template.AggregatesModel.Template> GetTemplateByIdAsync(string id)
		{
			return await _dbContext.TemplateCollection
									.AsQueryable()
									.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task SetTemplateDeletedStatusAsync(string userId, IList<string> templateIds, bool isDeleted)
		{
			var filter = Builders<Domain.Template.AggregatesModel.Template>.Filter.Where(x => templateIds.Contains(x.Id));
			var update = Builders<Domain.Template.AggregatesModel.Template>.Update.Set(x => x.ModifiedDate, DateTime.Now)
																		 .Set(x => x.IsDeleted, isDeleted)
																		 .Set(x => x.ModifiedByUserId, userId);

			await _dbContext.TemplateCollection.UpdateManyAsync(filter, update);
		}

		public async Task CreateTemplateAsync(Domain.Template.AggregatesModel.Template template)
		{
			await _dbContext.TemplateCollection.InsertOneAsync(template);
		}

		public async Task UpdateTemplateStatusAsync(string id, Domain.Template.AggregatesModel.TemplateStatus status, string userId)
		{
			var filter = Builders<Domain.Template.AggregatesModel.Template>.Filter.Where(x => id == x.Id);
			var update = Builders<Domain.Template.AggregatesModel.Template>.Update.Set(x => x.ModifiedDate, DateTime.Now)
																		 .Set(x => x.ModifiedByUserId, userId)
																		 .Set(x => x.Status, status);

			await _dbContext.TemplateCollection.UpdateOneAsync(filter, update);
		}

		public async Task UpdateEmailTemplateAsync(Domain.Template.AggregatesModel.EmailTemplate emailTemplate)
		{
			var filter = Builders<EmailTemplate>.Filter.Where(x => x.Id == emailTemplate.Id);
			var update = Builders<EmailTemplate>.Update.Set(x => x.ModifiedByUserId, emailTemplate.ModifiedByUserId)
																		 .Set(x => x.ModifiedDate, DateTime.Now)
																		 .Set(x => x.Name, emailTemplate.Name)
																		 .Set(x => x.Subject, emailTemplate.Subject)
																		 .Set(x => x.Body, emailTemplate.Body)
																		 .Set(x => x.Type, emailTemplate.Type)
																		 .Set(x => x.SubType, emailTemplate.SubType);

			await _dbContext.TemplateCollection.OfType<EmailTemplate>().UpdateOneAsync(filter, update);
		}

		public async Task<IList<Domain.Template.AggregatesModel.Template>> GetTemplateByIdsAsync(IList<string> ids)
		{
			var templates = await _dbContext.TemplateCollection
							.AsQueryable()
							.Where(x => ids.Contains(x.Id))
							.ToListAsync();

			return templates;
		}

		public async Task UpdateInterviewPrepTemplateAsync(InterviewPrepTemplate interviewPrepTemplate)
		{
			var filter = Builders<InterviewPrepTemplate>.Filter.Where(x => x.Id == interviewPrepTemplate.Id);
			var update = Builders<InterviewPrepTemplate>.Update.Set(x => x.ModifiedByUserId, interviewPrepTemplate.ModifiedByUserId)
																					  .Set(x => x.ModifiedDate, DateTime.Now)
																					  .Set(x => x.Description, interviewPrepTemplate.Description)
																					  .Set(x => x.InterviewType, interviewPrepTemplate.InterviewType)
																					  .Set(x => x.Name, interviewPrepTemplate.Name);

			await _dbContext.TemplateCollection.OfType<InterviewPrepTemplate>().UpdateOneAsync(filter, update);
		}

		public async Task UpdateJobDetailTemplateAsync(JobDetailTemplate jobTemplate)
		{
			var filter = Builders<JobDetailTemplate>.Filter.Where(x => x.Id == jobTemplate.Id);
			var update = Builders<JobDetailTemplate>.Update.Set(x => x.ModifiedByUserId, jobTemplate.ModifiedByUserId)
																				  .Set(x => x.ModifiedDate, DateTime.Now)
																				  .Set(x => x.PositionLevel, jobTemplate.PositionLevel)
																				  .Set(x => x.JobType, jobTemplate.JobType)
																				  .Set(x => x.Name, jobTemplate.Name)
																				  .Set(x => x.Summary, jobTemplate.Summary)
																				  .Set(x => x.Description, jobTemplate.Description)
																				  .Set(x => x.ContactPerson, jobTemplate.ContactPerson)
																				  .Set(x => x.JobCategoryIds, jobTemplate.JobCategoryIds);

			await _dbContext.TemplateCollection.OfType<JobDetailTemplate>().UpdateOneAsync(filter, update);
		}

		public async Task UpdateTemplateAttachmentAsync(EmailTemplate template)
		{
			var filter = Builders<EmailTemplate>.Filter.Where(x => x.Id == template.Id);

			var update = Builders<EmailTemplate>.Update.Set(x => x.Attachments, template.Attachments);

			await _dbContext.TemplateCollection.OfType<EmailTemplate>().UpdateOneAsync(filter, update);
		}
	}
}
