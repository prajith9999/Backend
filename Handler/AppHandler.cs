using LandWind.Models;
using LandWind.Repositories;

namespace LandWind.Handlers
{
    public class AppHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IBodyRepository _bodyRepository;
        private readonly IFaqRepository _faqRepository;
        private readonly IPageContentRepository _pageContentRepository;
        private readonly IFooterRepository _footerRepository;
        private readonly IFeatureRepository _featureRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly ISubscriptionDetailsRepository _subscriptionDetailsRepository;

        public AppHandler(
            IUserRepository userRepository,
            IBodyRepository bodyRepository,
            IFaqRepository faqRepository,
            IPageContentRepository pageContentRepository,
            IFooterRepository footerRepository,
            IFeatureRepository featureRepository,
            ISubscriptionRepository subscriptionRepository,
            ISubscriptionDetailsRepository subscriptionDetailsRepository
        )
        {
            _userRepository = userRepository;
            _bodyRepository = bodyRepository;
            _faqRepository = faqRepository;
            _pageContentRepository = pageContentRepository;
            _footerRepository = footerRepository;
            _featureRepository = featureRepository;
            _subscriptionRepository = subscriptionRepository;
            _subscriptionDetailsRepository = subscriptionDetailsRepository;
        }

        // User CRUD Operations
        public async Task<List<User>> GetUsers() => await _userRepository.GetAllUsers();
        public async Task<User> GetUserById(int id) => await _userRepository.GetUserById(id);
        public async Task<User> CreateUser(User user) => await _userRepository.InsertUser(user);
        public async Task<User> UpdateUser(int id, User user) => await _userRepository.UpdateUser(id, user);
        public async Task<bool> DeleteUser(int id) => await _userRepository.DeleteUser(id);

        // Body CRUD Operations
        public async Task<List<Body>> GetBodies() => await _bodyRepository.GetAllBodies();
        public async Task<Body> GetBodyById(int id) => await _bodyRepository.GetBodyById(id);
        public async Task<Body> CreateBody(Body body) => await _bodyRepository.InsertBody(body);
        public async Task<Body> UpdateBody(int id, Body body) => await _bodyRepository.UpdateBody(id, body);
        public async Task<bool> DeleteBody(int id) => await _bodyRepository.DeleteBody(id);

        // FAQ CRUD Operations
        public async Task<List<Faq>> GetFAQs() => await _faqRepository.GetAllFAQs();
        public async Task<Faq> GetFAQById(int id) => await _faqRepository.GetFAQById(id);
        public async Task<Faq> CreateFAQ(Faq faq) => await _faqRepository.InsertFAQ(faq);
        public async Task<Faq> UpdateFAQ(int id, Faq faq) => await _faqRepository.UpdateFAQ(id, faq);
        public async Task<bool> DeleteFAQ(int id) => await _faqRepository.DeleteFAQ(id);

        // PageContent CRUD Operations
        public async Task<List<PageContent>> GetPageContents() => await _pageContentRepository.GetAllPageContents();
        public async Task<PageContent> GetPageContentById(int id) => await _pageContentRepository.GetPageContentById(id);
        public async Task<PageContent> CreatePageContent(PageContent pageContent) => await _pageContentRepository.InsertPageContent(pageContent);
        public async Task<PageContent> UpdatePageContent(int id, PageContent pageContent) => await _pageContentRepository.UpdatePageContent(id, pageContent);
        public async Task<bool> DeletePageContent(int id) => await _pageContentRepository.DeletePageContent(id);

        // Footer CRUD Operations
        public async Task<List<Footer>> GetFooters() => await _footerRepository.GetAllFooters();
        public async Task<Footer> GetFooterById(int id) => await _footerRepository.GetFooterById(id);
        public async Task<Footer> CreateFooter(Footer footer) => await _footerRepository.InsertFooter(footer);
        public async Task<Footer> UpdateFooter(int id, Footer footer) => await _footerRepository.UpdateFooter(id, footer);
        public async Task<bool> DeleteFooter(int id) => await _footerRepository.DeleteFooter(id);

        // Feature CRUD Operations
        public async Task<List<Feature>> GetFeatures() => await _featureRepository.GetAllFeatures();
        public async Task<Feature> GetFeatureById(int id) => await _featureRepository.GetFeatureById(id);
        public async Task<Feature> CreateFeature(Feature feature) => await _featureRepository.InsertFeature(feature);
        public async Task<Feature> UpdateFeature(int id, Feature feature) => await _featureRepository.UpdateFeature(id, feature);
        public async Task<bool> DeleteFeature(int id) => await _featureRepository.DeleteFeature(id);

        // Subscription CRUD Operations
        public async Task<List<Subscription>> GetSubscriptions() => await _subscriptionRepository.GetAllSubscriptions();
        public async Task<Subscription> GetSubscriptionById(int id) => await _subscriptionRepository.GetSubscriptionById(id);
        public async Task<Subscription> CreateSubscription(Subscription subscription) => await _subscriptionRepository.InsertSubscription(subscription);
        public async Task<Subscription> UpdateSubscription(int id, Subscription subscription) => await _subscriptionRepository.UpdateSubscription(id, subscription);
        public async Task<bool> DeleteSubscription(int id) => await _subscriptionRepository.DeleteSubscription(id);

        // SubscriptionDetails CRUD Operations
        public async Task<List<SubscriptionDetails>> GetSubscriptionDetails() => await _subscriptionDetailsRepository.GetAllSubscriptionDetails();
        public async Task<SubscriptionDetails> GetSubscriptionDetailById(int id) => await _subscriptionDetailsRepository.GetSubscriptionDetailById(id);
        public async Task<SubscriptionDetails> CreateSubscriptionDetail(SubscriptionDetails subscriptionDetails) => await _subscriptionDetailsRepository.InsertSubscriptionDetail(subscriptionDetails);
        public async Task<SubscriptionDetails> UpdateSubscriptionDetail(int id, SubscriptionDetails subscriptionDetails) => await _subscriptionDetailsRepository.UpdateSubscriptionDetail(id, subscriptionDetails);
        public async Task<bool> DeleteSubscriptionDetail(int id) => await _subscriptionDetailsRepository.DeleteSubscriptionDetail(id);
    }
}
