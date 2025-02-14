using Microsoft.AspNetCore.Mvc;
using LandWind.Models;
using LandWind.Handlers;
using LandWind.Interfaces;
using Dapper;
using LandWind.Handler;
using LandWind.Repositories;



namespace LandWind.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BodyController : ControllerBase
    {
        private readonly IBodyHandler _bodyHandler;

        public BodyController(IBodyHandler bodyHandler)
        {
            _bodyHandler = bodyHandler;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Body>>> GetBodies()
        {
            try
            {
                var bodies = await _bodyHandler.GetBodies();
                //if (!(bodies != null && ((IEnumerable<Body>)bodies).Any())) return NotFound();
                return Ok(bodies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Body>> GetBody(int id)
        {
            try
            {
                var body = await _bodyHandler.GetBodyById(id);
                return body == null ? NotFound() : Ok(body);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Body>> PostBody(Body body)
        {
            if (body == null)
            {
                return BadRequest("Body cannot be null");
            }

            try
            {
                var createdBody = await _bodyHandler.InsertBody(body);
                return CreatedAtAction(nameof(GetBody), new { id = createdBody.ID }, createdBody);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Body>> PutBody(int id, Body body)
        {
            if (body == null)
            {
                return BadRequest("Body cannot be null");
            }

            try
            {
                var updatedBody = await _bodyHandler.UpdateBody(id, body);
                return updatedBody == null ? NotFound() : Ok(updatedBody);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBody(int id)
        {
            try
            {
                var isDeleted = await _bodyHandler.DeleteBody(id);
                return isDeleted ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class FaqController : ControllerBase
    {
        private readonly IFaqHandler _faqHandler;

        public FaqController(IFaqHandler faqHandler)
        {
            _faqHandler = faqHandler;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Faq>>> GetFaqs()
        {
            try
            {
                var faqs = await _faqHandler.GetAllFaqs();
                if (faqs == null || !faqs.Any()) return NotFound();
                return Ok(faqs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Faq>> GetFAQ(int id)
        {
            try
            {
                var faq = await _faqHandler.GetFaqById(id);
                return faq == null ? NotFound() : Ok(faq);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }





        [HttpPost]
        public async Task<ActionResult<Faq>> PostFAQ(Faq faq)
        {
            if (faq == null)
            {
                return BadRequest("FAQ cannot be null");
            }

            try
            {
                var createdFAQ = await _faqHandler.CreateFaq(faq);
                return CreatedAtAction(nameof(GetFAQ), new { id = createdFAQ.ID }, createdFAQ);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Faq>> PutFAQ(int id, Faq faq)
        {
            if (faq == null)
            {
                return BadRequest("FAQ cannot be null");
            }

            try
            {
                var updatedFAQ = await _faqHandler.UpdateFaq(id, faq);
                return updatedFAQ == null ? NotFound() : Ok(updatedFAQ);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFAQ(int id)
        {
            try
            {
                var isDeleted = await _faqHandler.DeleteFaq(id);
                return isDeleted ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

    // Add the same changes for FeaturePageController, FooterController, PageContentController, 
    // SubscriptionController, SubscriptionDetailsController, and SocialMediaController.

    [Route("api/[controller]")]
    [ApiController]
    public class FeaturePageController : ControllerBase
    {
        private readonly IFeaturePageHandler _featurePageHandler;

        public FeaturePageController(IFeaturePageHandler featurePageHandler)
        {
            _featurePageHandler = featurePageHandler;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeaturePage>>> GetFeatures()
        {
            try
            {
                var features = await _featurePageHandler.GetFeaturePages();
                if (features == null || !features.Any()) return NotFound();
                return Ok(features);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FeaturePage>> GetFeature(int id)
        {
            try
            {
                var feature = await _featurePageHandler.GetFeaturePageById(id);
                return feature == null ? NotFound() : Ok(feature);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<FeaturePage>> PostFeature(FeaturePage feature)
        {
            if (feature == null)
            {
                return BadRequest("Feature cannot be null");
            }

            try
            {
                var createdFeature = await _featurePageHandler.CreateFeaturePage(feature);
                return CreatedAtAction(nameof(GetFeature), new { id = createdFeature.ID }, createdFeature);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<FeaturePage>> PutFeature(int id, FeaturePage feature)
        {
            if (feature == null)
            {
                return BadRequest("Feature cannot be null");
            }

            try
            {
                var updatedFeature = await _featurePageHandler.UpdateFeaturePage(id, feature);
                return updatedFeature == null ? NotFound() : Ok(updatedFeature);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFeature(int id)
        {
            try
            {
                var isDeleted = await _featurePageHandler.DeleteFeaturePage(id);
                return isDeleted ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }



}

[Route("api/[controller]")]
[ApiController]
public class FooterController : ControllerBase
{
    private readonly IFooterHandler _footerHandler;

    // Constructor to inject the IFooterHandler
    public FooterController(IFooterHandler footerHandler)
    {
        _footerHandler = footerHandler;
    }

    // Get all footers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Footer>>> GetFooters()
    {
        try
        {
            var footers = await _footerHandler.GetFooter();
            return Ok(footers);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // Get a footer by ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Footer>> GetFooter(int id)
    {
        try
        {
            var footer = await _footerHandler.GetFooterById(id);
            return footer == null ? NotFound() : Ok(footer);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // Create a new footer
    [HttpPost]
    public async Task<ActionResult<Footer>> PostFooter(Footer footer)
    {
        if (footer == null)
        {
            return BadRequest("Footer cannot be null");
        }

        try
        {
            var createdFooter = await _footerHandler.CreateFooter(footer);
            return CreatedAtAction(nameof(GetFooter), new { id = createdFooter.ID }, createdFooter);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // Update an existing footer
    [HttpPut("{id}")]
    public async Task<ActionResult<Footer>> PutFooter(int id, Footer footer)
    {
        if (footer == null)
        {
            return BadRequest("Footer cannot be null");
        }

        try
        {
            var updatedFooter = await _footerHandler.UpdateFooter(id, footer);
            return updatedFooter == null ? NotFound() : Ok(updatedFooter);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // Delete a footer by ID
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteFooter(int id)
    {
        try
        {
            var isDeleted = await _footerHandler.DeleteFooter(id);
            if (!isDeleted)
            {
                return NotFound(); // Return NotFound if deletion failed
            }
            return NoContent(); // Successfully deleted, return NoContent status
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}


[Route("api/[controller]")]
    [ApiController]
    public class PageContentController : Controller
    {
        private readonly IPageContentHandler _pageContentHandler;

        public PageContentController(IPageContentHandler pageContentHandler)
        {
            _pageContentHandler = pageContentHandler;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PageContent>>> GetPageContents()
        {
            try
            {
                var pageContents = await _pageContentHandler.GetAllPageContent();
                return Ok(pageContents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PageContent>> GetPageContent(int id)
        {
            try
            {
                var pageContent = await _pageContentHandler.GetPageContentById(id);
                return pageContent == null ? NotFound() : Ok(pageContent);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<PageContent>> PostPageContent(PageContent pageContent)
        {
            if (pageContent == null)
            {
                return BadRequest("PageContent cannot be null");
            }

            try
            {
                var createdPageContent = await _pageContentHandler.CreatePageContent(pageContent);
                var pageContent1 = createdPageContent as PageContent;
                return CreatedAtAction(nameof(GetPageContent), new { id = pageContent.ID }, pageContent1);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PageContent>> PutPageContent(int id, PageContent pageContent)
        {
            if (pageContent == null)
            {
                return BadRequest("PageContent cannot be null");
            }

            try
            {
                var updatedPageContent = await _pageContentHandler.UpdatePageContent(id, pageContent);
                return updatedPageContent == null ? NotFound() : Ok(updatedPageContent);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePageContent(int id)
        {
            try
            {
                var isDeleted = await _pageContentHandler.DeletePageContent(id);
                return isDeleted == null ? NotFound() : NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : Controller
    {
        private readonly ISubscriptionHandler _subscriptionHandler;

        public SubscriptionController(ISubscriptionHandler subscriptionHandler)
        {
            _subscriptionHandler = subscriptionHandler;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subscription>>> GetSubscriptions()
        {
            try
            {
                var subscriptions = await _subscriptionHandler.GetSubscriptions();
                return Ok(subscriptions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Subscription>> GetSubscription(int id)
        {
            try
            {
                var subscription = await _subscriptionHandler.GetSubscriptionById(id);
                return subscription == null ? NotFound() : Ok(subscription);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Subscription>> PostSubscription(Subscription subscription)
        {
            if (subscription == null)
            {
                return BadRequest("Subscription cannot be null");
            }

            try
            {
                var createdSubscription = await _subscriptionHandler.CreateSubscription(subscription);
                var subscription1 = createdSubscription as Subscription;
                return CreatedAtAction(nameof(GetSubscription), new { id = subscription.ID }, subscription1);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Subscription>> PutSubscription(int id, Subscription subscription)
        {
            if (subscription == null)
            {
                return BadRequest("Subscription cannot be null");
            }

            try
            {
                var updatedSubscription = await _subscriptionHandler.UpdateSubscription(id, subscription);
                return updatedSubscription == null ? NotFound() : Ok(updatedSubscription);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSubscription(int id)
        {
            try
            {
                var isDeleted = await _subscriptionHandler.DeleteSubscription(id);
                return isDeleted == null ? NotFound() : NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionDetailsController : Controller
    {
        private readonly ISubscriptionHandlerDetail _subscriptionDetailsHandler;

        public SubscriptionDetailsController(ISubscriptionHandlerDetail subscriptionDetailsHandler)
        {
            _subscriptionDetailsHandler = subscriptionDetailsHandler;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubscriptionDetails>>> GetSubscriptionDetails()
        {
            try
            {
                var subscriptionDetails = await _subscriptionDetailsHandler.GetSubscriptions();
                return Ok(subscriptionDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SubscriptionDetails>> GetSubscriptionDetail(int id)
        {
            try
            {
                var subscriptionDetail = await _subscriptionDetailsHandler.GetSubscriptionById(id);
                return subscriptionDetail == null ? NotFound() : Ok(subscriptionDetail);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<SubscriptionDetails>> PostSubscriptionDetail(SubscriptionDetails subscriptionDetails)
        {
            if (subscriptionDetails == null)
            {
                return BadRequest("SubscriptionDetail cannot be null");
            }

            try
            {
                var createdSubscriptionDetail = await _subscriptionDetailsHandler.CreateSubscription(subscriptionDetails);
                var subscriptionDetails1 = createdSubscriptionDetail as SubscriptionDetails;
                return CreatedAtAction(nameof(GetSubscriptionDetail), new { id = subscriptionDetails.ID }, subscriptionDetails1);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SubscriptionDetails>> PutSubscriptionDetail(int id, SubscriptionDetails subscriptionDetails)
        {
            if (subscriptionDetails == null)
            {
                return BadRequest("SubscriptionDetail cannot be null");
            }

            try
            {
                var updatedSubscriptionDetail = await _subscriptionDetailsHandler.UpdateSubscription(id, subscriptionDetails);
                return updatedSubscriptionDetail == null ? NotFound() : Ok(updatedSubscriptionDetail);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSubscriptionDetail(int id)
        {
            try
            {
                var isDeleted = await _subscriptionDetailsHandler.DeleteSubscription(id);
                return isDeleted == null ? NotFound() : NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

[Route("api/[controller]")]
[ApiController]
public class SocialMediaController : Controller
{
    private readonly ISocialMediaHandler _socialMediaHandler;

    public SocialMediaController(ISocialMediaHandler socialMediaHandler)
    {
        _socialMediaHandler = socialMediaHandler;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SocialMedia>>> GetSocialMedias()
    {
        try
        {
            var socialMedias = await _socialMediaHandler.GetSocialMedia();
            return Ok(socialMedias);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SocialMedia>> GetSocialMedia(int id)
    {
        try
        {
            var socialMedia = await _socialMediaHandler.GetSocialMediaById(id);
            return socialMedia == null ? NotFound() : Ok(socialMedia);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult<SocialMedia>> PostSocialMedia(SocialMedia socialMedia)
    {
        if (socialMedia == null)
        {
            return BadRequest("SocialMedia cannot be null");
        }

        try
        {
            var createdSocialMedia = await _socialMediaHandler.CreateSocialMedia(socialMedia);
            var socialMedia1 = createdSocialMedia as SocialMedia;
            return CreatedAtAction(nameof(GetSocialMedia), new { id = socialMedia.ID }, socialMedia1);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<SocialMedia>> PutSocialMedia(int id, SocialMedia socialMedia)
    {
        if (socialMedia == null)
        {
            return BadRequest("SocialMedia cannot be null");
        }

        try
        {
            var updatedSocialMedia = await _socialMediaHandler.UpdateSocialMedia(id, socialMedia);
            return updatedSocialMedia == null ? NotFound() : Ok(updatedSocialMedia);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteSocialMedia(int id)
    {
        try
        {
            var isDeleted = await _socialMediaHandler.DeleteSocialMedia(id);
            return isDeleted == null ? NotFound() : NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
        



////[Route("api/[controller]")]
////[ApiController]
////public class UserController : Controller
////{
////    private readonly IUserHandler _userHandler;

////    public UserController(IUserHandler userHandler)
////    {
////        _userHandler = userHandler;
////    }

////    [HttpGet]
////    public async Task<ActionResult<IEnumerable<User>>> GetUsers() =>
////        Ok(await _userHandler.GetUsers());

////    [HttpGet("{id}")]
////    public async Task<ActionResult<User>> GetUser(int id)
////    {
////        var user = await _userHandler.GetUserById(id);
////        return user == null ? NotFound() : Ok(user);
////    }

////    [HttpPost]
////    public async Task<ActionResult<User>> PostUser(User user)
////    {
////        var createdUser = await _userHandler.InsertUser(user);
////        return CreatedAtAction(nameof(GetUser), new { id = createdUser.ID }, createdUser);
////    }

////    [HttpPut("{id}")]
////    public async Task<ActionResult<User>> PutUser(int id, User user)
////    {
////        var updatedUser = await _userHandler.UpdateUser(id, user);
////        return updatedUser == null ? NotFound() : Ok(updatedUser);
////    }

////    [HttpDelete("{id}")]
////    public async Task<ActionResult> DeleteUser(int id)
////    {
////        var isDeleted = await _userHandler.DeleteUser(id);
////        return !isDeleted ? NotFound() : NoContent();
////    }
////}

