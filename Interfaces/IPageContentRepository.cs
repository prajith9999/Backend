using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LandWind.Models;
using LandWind.Interfaces;

namespace LandWind.Interfaces
{
    public interface IPageContentRepository
    {
        Task<List<PageContent>> GetAll();
        Task<PageContent> GetById(int id);
        Task<PageContent> Create(PageContent item);
        Task<PageContent> Update(int id, PageContent item);
        Task<bool> Delete(int id);
    }
}
