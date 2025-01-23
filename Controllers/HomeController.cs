using AutoMapper.Features;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using vueproject_asp.Models;
using vueproject_asp.Repositories;

namespace vueproject_asp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppController : ControllerBase
    {
        private readonly AppHandler _appHandler;

        public AppController(AppHandler appHandler)
        {
            _appHandler = appHandler;
        }

        // PageContent CRUD Operations
        [HttpGet("page-content")]
        public async Task<ActionResult<IEnumerable<PageContent>>> GetPageContents()
        {
            var pageContents = await _appHandler.GetPageContents();
            return Ok(pageContents);
        }

        [HttpGet("page-content/{id}")]
        public async Task<ActionResult<PageContent>> GetPageContent(int id)
        {
            var pageContent = await _appHandler.GetPageContentById(id);
            if (pageContent == null)
            {
                return NotFound();
            }
            return Ok(pageContent);
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
            if (updatedPageContent == null)
            {
                return NotFound();
            }
            return Ok(updatedPageContent);
        }

        [HttpDelete("page-content/{id}")]
        public async Task<ActionResult> DeletePageContent(int id)
        {
            var isDeleted = await _appHandler.DeletePageContent(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        // Feature CRUD Operations
        [HttpGet("feature")]
        public async Task<ActionResult<IEnumerable<FeaturePage>>> GetFeatures()
        {
            var features = await _appHandler.GetFeatures();
            return Ok(features);
        }

        [HttpGet("feature/{id}")]
        public async Task<ActionResult<FeaturePage>> GetFeature(int id)
        {
            var feature = await _appHandler.GetFeatureById(id);
            if (feature == null)
            {
                return NotFound();
            }
            return Ok(feature);
        }

        [HttpPost("feature")]
        public async Task<ActionResult<FeaturePage>> PostFeature(FeaturePage feature) => CreatedAtAction(nameof(GetFeature), new NewRecord((await _appHandler.InsertFeature(feature)).ID), await _appHandler.InsertFeature(feature));

        [HttpPut("feature/{id}")]
        public async Task<ActionResult<FeaturePage>> PutFeature(int id, FeaturePage feature)
        {
            if (await _appHandler.UpdateFeature(id, feature) == null)
            {
                return NotFound();
            }
            return Ok(await _appHandler.UpdateFeature(id, feature));
        }

        [HttpDelete("feature/{id}")]
        public async Task<ActionResult> DeleteFeature(int id)
        {
            var isDeleted = await _appHandler.DeleteFeature(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        // Body CRUD Operations
        [HttpGet("body")]
        public async Task<ActionResult<IEnumerable<Body>>> GetBodies()
        {
            var bodies = await _appHandler.GetBodies();
            return Ok(bodies);
        }

        [HttpGet("body/{id}")]
        public async Task<ActionResult<Body>> GetBody(int id)
        {
            var body = await _appHandler.GetBodyById(id);
            if (body == null)
            {
                return NotFound();
            }
            return Ok(body);
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
            if (updatedBody == null)
            {
                return NotFound();
            }
            return Ok(updatedBody);
        }

        [HttpDelete("body/{id}")]
        public async Task<ActionResult> DeleteBody(int id)
        {
            var isDeleted = await _appHandler.DeleteBody(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        // FAQ CRUD Operations
        [HttpGet("faq")]
        public async Task<ActionResult<IEnumerable<Faq>>> GetFaq()
        {
            var faqs = await _appHandler.GetFAQs();
            return Ok(faqs);
        }

        [HttpGet("faq/{id}")]
        public async Task<ActionResult<Faq>> GetFAQ(int id)
        {
            var faq = await _appHandler.GetFAQById(id);
            if (faq == null)
            {
                return NotFound();
            }
            return Ok(faq);
        }

        [HttpPost("faq")]
        public async Task<ActionResult<Faq>> PostFAQ(Faq faq, Faq faq)
        {
            if (faq is null)
            {
                throw new ArgumentNullException(nameof(faq));
            }

            if (faq is null)
            {
                throw new ArgumentNullException(nameof(faq));
            }

            if (faq is null)
            {
                throw new ArgumentNullException(nameof(faq));
            }

            if (faq is null)
            {
                throw new ArgumentNullException(nameof(faq));
            }

            if (faq is null)
            {
                throw new ArgumentNullException(nameof(faq));
            }

            var createdFAQ = await _appHandler.InsertFAQ(faq: faq);
            return CreatedAtAction(nameof(GetFAQ), new { id = createdFAQ.ID }, createdFAQ);
        }

        [HttpPut("faq/{id}")]
        public async Task<ActionResult<Faq>> PutFAQ(int id, Faq faq)
        {
            var updatedFAQ = await _appHandler.UpdateFAQ(id, faq: Faq);
            if (updatedFAQ == null)
            {
                return NotFound();
            }
            return Ok(updatedFAQ);
        }

        [HttpDelete("faq/{id}")]
        public async Task<ActionResult> DeleteFAQ(int id)
        {
            var isDeleted = await _appHandler.DeleteFAQ(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        // Footer CRUD Operations
        [HttpGet("footer")]
        public async Task<ActionResult<IEnumerable<Footer>>> GetFooters()
        {
            var footers = await _appHandler.GetFooters();
            return Ok(footers);
        }

        [HttpGet("footer/{id}")]
        public async Task<ActionResult<Footer>> GetFooter(int id)
        {
            var footer = await _appHandler.GetFooterById(id);
            if (footer == null)
            {
                return NotFound();
            }
            return Ok(footer);
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
            if (updatedFooter == null)
            {
                return NotFound();
            }
            return Ok(updatedFooter);
        }

        [HttpDelete("footer/{id}")]
        public async Task<ActionResult> DeleteFooter(int id)
        {
            var isDeleted = await _appHandler.DeleteFooter(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        // Subscription CRUD Operations
        [HttpGet("subscription")]
        public async Task<ActionResult<IEnumerable<Subscription>>> GetSubscriptions()
        {
            var subscriptions = await _appHandler.GetSubscriptions();
            return Ok(subscriptions);
        }

        [HttpGet("subscription/{id}")]
        public async Task<ActionResult<Subscription>> GetSubscription(int id)
        {
            var subscription = await _appHandler.GetSubscriptionById(id);
            if (subscription == null)
            {
                return NotFound();
            }
            return Ok(subscription);
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
            if (updatedSubscription == null)
            {
                return NotFound();
            }
            return Ok(updatedSubscription);
        }

        [HttpDelete("subscription/{id}")]
        public async Task<ActionResult> DeleteSubscription(int id)
        {
            var isDeleted = await _appHandler.DeleteSubscription(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        // SubscriptionDetails CRUD Operations
        [HttpGet("subscription-details")]
        public async Task<ActionResult<IEnumerable<SubscriptionDetails>>> GetSubscriptionDetails()
        {
            var subscriptionDetails = await _appHandler.GetSubscriptionDetails();
            return Ok(subscriptionDetails);
        }

        [HttpGet("subscription-details/{id}")]
        public async Task<ActionResult<SubscriptionDetails>> GetSubscriptionDetail(int id)
        {
            var subscriptionDetail = await _appHandler.GetSubscriptionDetailById(id);
            if (subscriptionDetail == null)
            {
                return NotFound();
            }
            return Ok(subscriptionDetail);
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
            if (updatedSubscriptionDetail == null)
            {
                return NotFound();
            }
            return Ok(updatedSubscriptionDetail);
        }

        [HttpDelete("subscription-details/{id}")]
        public async Task<ActionResult> DeleteSubscriptionDetail(int id)
        {
            var isDeleted = await _appHandler.DeleteSubscriptionDetail(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        // User CRUD Operations
        [HttpGet("user")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _appHandler.GetUsers();
            return Ok(users);
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _appHandler.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
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
            if (updatedUser == null)
            {
                return NotFound();
            }
            return Ok(updatedUser);
        }

        [HttpDelete("user/{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var isDeleted = await _appHandler.DeleteUser(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        // SocialMedia CRUD Operations
        [HttpGet("social-media")]
        public async Task<ActionResult<IEnumerable<SocialMedia>>> GetSocialMedias()
        {
            var socialMedias = await _appHandler.GetSocialMedias();
            return Ok(socialMedias);
        }

        [HttpGet("social-media/{id}")]
        public async Task<ActionResult<SocialMedia>> GetSocialMedia(int id)
        {
            var socialMedia = await _appHandler.GetSocialMediaById(id);
            if (socialMedia == null)
            {
                return NotFound();
            }
            return Ok(socialMedia);
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
            if (updatedSocialMedia == null)
            {
                return NotFound();
            }
            return Ok(updatedSocialMedia);
        }

        [HttpDelete("social-media/{id}")]
        public async Task<ActionResult> DeleteSocialMedia(int id)
        {
            var isDeleted = await _appHandler.DeleteSocialMedia(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }

    internal record NewRecord(object Id);
}
