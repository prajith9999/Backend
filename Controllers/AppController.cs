using AutoMapper.Features;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using LandWind.Models;
using LandWind.Repositories;

namespace LandWind.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppController : Controller
    {
        private readonly AppHandler _appHandler;

        public AppController(AppHandler appHandler)
        {
            _appHandler = appHandler;
        }

        // PageContent CRUD Operations
        [HttpGet("page-content")]
        public async Task<ActionResult<IEnumerable<PageContent>>> GetPageContents() =>
            Ok(await _appHandler.GetPageContents());

        [HttpGet("page-content/{id}")]
        public async Task<ActionResult<PageContent>> GetPageContent(int id)
        {
            var pageContent = await _appHandler.GetPageContentById(id);
            return pageContent == null ? NotFound() : Ok(pageContent);
        }

        [HttpPost("page-content")]
        public async Task<ActionResult<PageContent>> PostPageContent(PageContent pageContent)
        {
            var createdPageContent = await _appHandler.InsertPageContent(pageContent);
            return CreatedAtAction(nameof(GetPageContent), new { id = createdPageContent.ID }, createdPageContent);
        }

        [HttpPut("page-content/{id}")]
        public async Task<ActionResult<PageContent>> PutPageContent(int id, PageContent pageContent)
        {
            var updatedPageContent = await _appHandler.UpdatePageContent(id, pageContent);
            return updatedPageContent == null ? NotFound() : Ok(updatedPageContent);
        }

        [HttpDelete("page-content/{id}")]
        public async Task<ActionResult> DeletePageContent(int id)
        {
            var isDeleted = await _appHandler.DeletePageContent(id);
            return !isDeleted ? NotFound() : NoContent();
        }

        // Feature CRUD Operations
        [HttpGet("feature")]
        public async Task<ActionResult<IEnumerable<FeaturePage>>> GetFeatures() =>
            Ok(await _appHandler.GetFeatures());

        [HttpGet("feature/{id}")]
        public async Task<ActionResult<FeaturePage>> GetFeature(int id)
        {
            var feature = await _appHandler.GetFeatureById(id);
            return feature == null ? NotFound() : Ok(feature);
        }

        [HttpPost("feature")]
        public async Task<ActionResult<FeaturePage>> PostFeature(FeaturePage feature)
        {
            var createdFeature = await _appHandler.InsertFeature(feature);
            return CreatedAtAction(nameof(GetFeature), new { id = createdFeature.ID }, createdFeature);
        }

        [HttpPut("feature/{id}")]
        public async Task<ActionResult<FeaturePage>> PutFeature(int id, FeaturePage feature)
        {
            var updatedFeature = await _appHandler.UpdateFeature(id, feature);
            return updatedFeature == null ? NotFound() : Ok(updatedFeature);
        }

        [HttpDelete("feature/{id}")]
        public async Task<ActionResult> DeleteFeature(int id)
        {
            var isDeleted = await _appHandler.DeleteFeature(id);
            return !isDeleted ? NotFound() : NoContent();
        }

        // Body CRUD Operations
        [HttpGet("body")]
        public async Task<ActionResult<IEnumerable<Body>>> GetBodies() =>
            Ok(await _appHandler.GetBodies());

        [HttpGet("body/{id}")]
        public async Task<ActionResult<Body>> GetBody(int id)
        {
            var body = await _appHandler.GetBodyById(id);
            return body == null ? NotFound() : Ok(body);
        }

        [HttpPost("body")]
        public async Task<ActionResult<Body>> PostBody(Body body)
        {
            var createdBody = await _appHandler.InsertBody(body);
            return CreatedAtAction(nameof(GetBody), new { id = createdBody.ID }, createdBody);
        }

        [HttpPut("body/{id}")]
        public async Task<ActionResult<Body>> PutBody(int id, Body body)
        {
            var updatedBody = await _appHandler.UpdateBody(id, body);
            return updatedBody == null ? NotFound() : Ok(updatedBody);
        }

        [HttpDelete("body/{id}")]
        public async Task<ActionResult> DeleteBody(int id)
        {
            var isDeleted = await _appHandler.DeleteBody(id);
            return !isDeleted ? NotFound() : NoContent();
        }

        // FAQ CRUD Operations
        [HttpGet("faq")]
        public async Task<ActionResult<IEnumerable<Faq>>> GetFaqs() =>
            Ok(await _appHandler.GetFAQs());

        [HttpGet("faq/{id}")]
        public async Task<ActionResult<Faq>> GetFAQ(int id)
        {
            var faq = await _appHandler.GetFAQById(id);
            return faq == null ? NotFound() : Ok(faq);
        }

        [HttpPost("faq")]
        public async Task<ActionResult<Faq>> PostFAQ(Faq faq)
        {
            if (faq == null)
            {
                return BadRequest("FAQ cannot be null.");
            }

            var createdFAQ = await _appHandler.InsertFAQ(faq);
            return CreatedAtAction(nameof(GetFAQ), new { id = createdFAQ.ID }, createdFAQ);
        }

        [HttpPut("faq/{id}")]
        public async Task<ActionResult<Faq>> PutFAQ(int id, Faq faq)
        {
            var updatedFAQ = await _appHandler.UpdateFAQ(id, faq);
            return updatedFAQ == null ? NotFound() : Ok(updatedFAQ);
        }

        [HttpDelete("faq/{id}")]
        public async Task<ActionResult> DeleteFAQ(int id)
        {
            var isDeleted = await _appHandler.DeleteFAQ(id);
            return !isDeleted ? NotFound() : NoContent();
        }

        // Footer CRUD Operations
        [HttpGet("footer")]
        public async Task<ActionResult<IEnumerable<Footer>>> GetFooters() =>
            Ok(await _appHandler.GetFooters());

        [HttpGet("footer/{id}")]
        public async Task<ActionResult<Footer>> GetFooter(int id)
        {
            var footer = await _appHandler.GetFooterById(id);
            return footer == null ? NotFound() : Ok(footer);
        }

        [HttpPost("footer")]
        public async Task<ActionResult<Footer>> PostFooter(Footer footer)
        {
            var createdFooter = await _appHandler.InsertFooter(footer);
            return CreatedAtAction(nameof(GetFooter), new { id = createdFooter.ID }, createdFooter);
        }

        [HttpPut("footer/{id}")]
        public async Task<ActionResult<Footer>> PutFooter(int id, Footer footer)
        {
            var updatedFooter = await _appHandler.UpdateFooter(id, footer);
            return updatedFooter == null ? NotFound() : Ok(updatedFooter);
        }

        [HttpDelete("footer/{id}")]
        public async Task<ActionResult> DeleteFooter(int id)
        {
            var isDeleted = await _appHandler.DeleteFooter(id);
            return !isDeleted ? NotFound() : NoContent();
        }

        // Subscription CRUD Operations
        [HttpGet("subscription")]
        public async Task<ActionResult<IEnumerable<Subscription>>> GetSubscriptions() =>
            Ok(await _appHandler.GetSubscriptions());

        [HttpGet("subscription/{id}")]
        public async Task<ActionResult<Subscription>> GetSubscription(int id)
        {
            var subscription = await _appHandler.GetSubscriptionById(id);
            return subscription == null ? NotFound() : Ok(subscription);
        }

        [HttpPost("subscription")]
        public async Task<ActionResult<Subscription>> PostSubscription(Subscription subscription)
        {
            var createdSubscription = await _appHandler.InsertSubscription(subscription);
            return CreatedAtAction(nameof(GetSubscription), new { id = createdSubscription.ID }, createdSubscription);
        }

        [HttpPut("subscription/{id}")]
        public async Task<ActionResult<Subscription>> PutSubscription(int id, Subscription subscription)
        {
            var updatedSubscription = await _appHandler.UpdateSubscription(id, subscription);
            return updatedSubscription == null ? NotFound() : Ok(updatedSubscription);
        }

        [HttpDelete("subscription/{id}")]
        public async Task<ActionResult> DeleteSubscription(int id)
        {
            var isDeleted = await _appHandler.DeleteSubscription(id);
            return !isDeleted ? NotFound() : NoContent();
        }

        // SubscriptionDetails CRUD Operations
        [HttpGet("subscription-details")]
        public async Task<ActionResult<IEnumerable<SubscriptionDetails>>> GetSubscriptionDetails() =>
            Ok(await _appHandler.GetSubscriptionDetails());

        [HttpGet("subscription-details/{id}")]
        public async Task<ActionResult<SubscriptionDetails>> GetSubscriptionDetail(int id)
        {
            var subscriptionDetail = await _appHandler.GetSubscriptionDetailById(id);
            return subscriptionDetail == null ? NotFound() : Ok(subscriptionDetail);
        }

        [HttpPost("subscription-details")]
        public async Task<ActionResult<SubscriptionDetails>> PostSubscriptionDetail(SubscriptionDetails subscriptionDetails)
        {
            var createdSubscriptionDetail = await _appHandler.InsertSubscriptionDetail(subscriptionDetails);
            return CreatedAtAction(nameof(GetSubscriptionDetail), new { id = createdSubscriptionDetail.ID }, createdSubscriptionDetail);
        }

        [HttpPut("subscription-details/{id}")]
        public async Task<ActionResult<SubscriptionDetails>> PutSubscriptionDetail(int id, SubscriptionDetails subscriptionDetails)
        {
            var updatedSubscriptionDetail = await _appHandler.UpdateSubscriptionDetail(id, subscriptionDetails);
            return updatedSubscriptionDetail == null ? NotFound() : Ok(updatedSubscriptionDetail);
        }

        [HttpDelete("subscription-details/{id}")]
        public async Task<ActionResult> DeleteSubscriptionDetail(int id)
        {
            var isDeleted = await _appHandler.DeleteSubscriptionDetail(id);
            return !isDeleted ? NotFound() : NoContent();
        }

        // User CRUD Operations
        [HttpGet("user")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers() =>
            Ok(await _appHandler.GetUsers());

        [HttpGet("user/{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _appHandler.GetUserById(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost("user")]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            var createdUser = await _appHandler.InsertUser(user);
            return CreatedAtAction(nameof(GetUser), new { id = createdUser.ID }, createdUser);
        }

        [HttpPut("user/{id}")]
        public async Task<ActionResult<User>> PutUser(int id, User user)
        {
            var updatedUser = await _appHandler.UpdateUser(id, user);
            return updatedUser == null ? NotFound() : Ok(updatedUser);
        }

        [HttpDelete("user/{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var isDeleted = await _appHandler.DeleteUser(id);
            return !isDeleted ? NotFound() : NoContent();
        }

        // SocialMedia CRUD Operations
        [HttpGet("social-media")]
        public async Task<ActionResult<IEnumerable<SocialMedia>>> GetSocialMedias() =>
            Ok(await _appHandler.GetSocialMedias());

        [HttpGet("social-media/{id}")]
        public async Task<ActionResult<SocialMedia>> GetSocialMedia(int id)
        {
            var socialMedia = await _appHandler.GetSocialMediaById(id);
            return socialMedia == null ? NotFound() : Ok(socialMedia);
        }

        [HttpPost("social-media")]
        public async Task<ActionResult<SocialMedia>> PostSocialMedia(SocialMedia socialMedia)
        {
            var createdSocialMedia = await _appHandler.InsertSocialMedia(socialMedia);
            return CreatedAtAction(nameof(GetSocialMedia), new { id = createdSocialMedia.ID }, createdSocialMedia);
        }

        [HttpPut("social-media/{id}")]
        public async Task<ActionResult<SocialMedia>> PutSocialMedia(int id, SocialMedia socialMedia)
        {
            var updatedSocialMedia = await _appHandler.UpdateSocialMedia(id, socialMedia);
            return updatedSocialMedia == null ? NotFound() : Ok(updatedSocialMedia);
        }

        [HttpDelete("social-media/{id}")]
        public async Task<ActionResult> DeleteSocialMedia(int id)
        {
            var isDeleted = await _appHandler.DeleteSocialMedia(id);
            return !isDeleted ? NotFound() : NoContent();
        }
    }
}
