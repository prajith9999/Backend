using AutoMapper.Features;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using vueproject_asp.Models;

namespace vueproject_asp.Repositories
{
    public class AppHandler
    {
        private readonly IDbConnection _dbConnection;

        // Constructor to initialize the database connection
        public AppHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
        }

        // PageContent CRUD Operations
        public async Task<IEnumerable<PageContent>> GetPageContents()
        {
            return await GetAll<PageContent>("PageContents");
        }

        public async Task<PageContent> GetPageContentById(int id)
        {
            return await GetById<PageContent>("PageContents", id);
        }

        public async Task<PageContent> InsertPageContent(PageContent pageContent)
        {
            return await Insert("PageContents", pageContent);
        }

        public async Task<PageContent> UpdatePageContent(int id, PageContent pageContent)
        {
            return await Update("PageContents", id, pageContent);
        }

        public async Task<bool> DeletePageContent(int id)
        {
            return await Delete("PageContents", id);
        }

        // Feature CRUD Operations
        public async Task<IEnumerable<FeaturePage>> GetFeatures()
        {
            return await GetAll<FeaturePage>("Features");
        }

        public async Task<FeaturePage> GetFeatureById(int id)
        {
            return await GetById<FeaturePage>("Features", id);
        }

        public async Task<FeaturePage> InsertFeature(FeaturePage feature)
        {
            var query = $"INSERT INTO Features ({string.Join(",", GetColumns(feature))}) VALUES ({string.Join(",", GetColumnsWithPrefix(feature))}); SELECT LAST_INSERT_ID();";
            var id = await _dbConnection.ExecuteScalarAsync<int>(query, feature);
            SetId(feature, id);
            return feature;
        }

        public async Task<FeaturePage> UpdateFeature(int id, FeaturePage feature)
        {
            var query = $"UPDATE Features SET {string.Join(",", GetUpdateColumns(feature))} WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(query, new { feature, Id = id });
            return feature;
        }

        public async Task<bool> DeleteFeature(int id)
        {
            var query = $"DELETE FROM Features WHERE Id = @Id";
            var rowsAffected = await _dbConnection.ExecuteAsync(query, new { Id = id });
            return rowsAffected > 0;
        }

        // Body CRUD Operations
        public async Task<IEnumerable<Body>> GetBodies()
        {
            return await GetAll<Body>("Bodies");
        }

        public async Task<Body> GetBodyById(int id)
        {
            return await GetById<Body>("Bodies", id);
        }

        public async Task<Body> InsertBody(Body body)
        {
            return await Insert("Bodies", body);
        }

        public async Task<Body> UpdateBody(int id, Body body)
        {
            return await Update("Bodies", id, body);
        }

        public async Task<bool> DeleteBody(int id)
        {
            return await Delete("Bodies", id);
        }

        // FAQ CRUD Operations
        public async Task<IEnumerable<Faq>> GetFAQs()
        {
            return await GetAll<Faq>("FAQs");
        }

        public async Task<Faq> GetFAQById(int id)
        {
            return await GetById<Faq>("FAQs", id);
        }

        public async Task<Faq> InsertFAQ(Faq faq)
        {
            return await Insert("FAQs", faq);
        }

        public async Task<Faq> UpdateFAQ(int id, Faq faq)
        {
            return await Update("FAQs", id, faq);
        }

        public async Task<bool> DeleteFAQ(int id)
        {
            return await Delete("FAQs", id);
        }

        // Footer CRUD Operations
        public async Task<IEnumerable<Footer>> GetFooters()
        {
            return await GetAll<Footer>("Footers");
        }

        public async Task<Footer> GetFooterById(int id)
        {
            return await GetById<Footer>("Footers", id);
        }

        public async Task<Footer> InsertFooter(Footer footer)
        {
            return await Insert("Footers", footer);
        }

        public async Task<Footer> UpdateFooter(int id, Footer footer)
        {
            return await Update("Footers", id, footer);
        }

        public async Task<bool> DeleteFooter(int id)
        {
            return await Delete("Footers", id);
        }

        // Subscription CRUD Operations
        public async Task<IEnumerable<Subscription>> GetSubscriptions()
        {
            return await GetAll<Subscription>("Subscriptions");
        }

        public async Task<Subscription> GetSubscriptionById(int id)
        {
            return await GetById<Subscription>("Subscriptions", id);
        }

        public async Task<Subscription> InsertSubscription(Subscription subscription)
        {
            return await Insert("Subscriptions", subscription);
        }

        public async Task<Subscription> UpdateSubscription(int id, Subscription subscription)
        {
            return await Update("Subscriptions", id, subscription);
        }

        public async Task<bool> DeleteSubscription(int id)
        {
            return await Delete("Subscriptions", id);
        }

        // SubscriptionDetails CRUD Operations
        public async Task<IEnumerable<SubscriptionDetails>> GetSubscriptionDetails()
        {
            return await GetAll<SubscriptionDetails>("SubscriptionDetails");
        }

        public async Task<SubscriptionDetails> GetSubscriptionDetailById(int id)
        {
            return await GetById<SubscriptionDetails>("SubscriptionDetails", id);
        }

        public async Task<SubscriptionDetails> InsertSubscriptionDetail(SubscriptionDetails subscriptionDetails)
        {
            return await Insert("SubscriptionDetails", subscriptionDetails);
        }

        public async Task<SubscriptionDetails> UpdateSubscriptionDetail(int id, SubscriptionDetails subscriptionDetails)
        {
            return await Update("SubscriptionDetails", id, subscriptionDetails);
        }

        public async Task<bool> DeleteSubscriptionDetail(int id)
        {
            return await Delete("SubscriptionDetails", id);
        }

        // User CRUD Operations
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await GetAll<User>("Users");
        }

        public async Task<User> GetUserById(int id)
        {
            return await GetById<User>("Users", id);
        }

        public async Task<User> InsertUser(User user)
        {
            return await Insert("Users", user);
        }

        public async Task<User> UpdateUser(int id, User user)
        {
            return await Update("Users", id, user);
        }

        public async Task<bool> DeleteUser(int id)
        {
            var query = $"DELETE FROM Users WHERE Id = @Id";
            var rowsAffected = await _dbConnection.ExecuteAsync(query, new { Id = id });
            return rowsAffected > 0;
        }

        // SocialMedia CRUD Operations
        public async Task<IEnumerable<SocialMedia>> GetSocialMedias()
        {
            return await GetAll<SocialMedia>("SocialMedias");
        }

        public async Task<SocialMedia> GetSocialMediaById(int id)
        {
            return await GetById<SocialMedia>("SocialMedias", id);
        }

        public async Task<SocialMedia> InsertSocialMedia(SocialMedia socialMedia)
        {
            return await Insert("SocialMedias", socialMedia);
        }

        public async Task<SocialMedia> UpdateSocialMedia(int id, SocialMedia socialMedia)
        {
            return await Update("SocialMedias", id, socialMedia);
        }

        public async Task<bool> DeleteSocialMedia(int id)
        {
            return await Delete("SocialMedias", id);
        }

        // Generic method to get all records of a specific entity
        private async Task<IEnumerable<T>> GetAll<T>(string tableName)
        {
            var query = $"SELECT * FROM {tableName}";
            return await _dbConnection.QueryAsync<T>(query);
        }

        // Generic method to get a record by ID
        private async Task<T> GetById<T>(string tableName, int id)
        {
            var query = $"SELECT * FROM {tableName} WHERE Id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<T>(query, new { Id = id });
        }

        // Generic method to insert a new record
        private async Task<T> Insert<T>(string tableName, T entity)
        {
            var columns = GetColumns(