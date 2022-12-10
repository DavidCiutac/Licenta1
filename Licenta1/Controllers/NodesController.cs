using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Licenta1.Contracts;
using Licenta1.Data;
using Licenta1.Models;

namespace Licenta1.Controllers
{
    public class NodesController : Controller
    {
        
        private readonly INodeRepository nodeRepository;
        private readonly IMapper mapper;
        private readonly IStationsRepository stationsRepository;

        public NodesController(ApplicationDbContext context, INodeRepository nodeRepository, IMapper mapper, IStationsRepository stationsRepository)
        {

            this.nodeRepository = nodeRepository;
            this.mapper = mapper;
            this.stationsRepository = stationsRepository;
        }

        // GET: Nodes
        public async Task<IActionResult> Index()
        {
            var model = mapper.Map<List<NodeVM>>(await nodeRepository.GetNodesAsync());
            return View(model);
        }
       

        // GET: Nodes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var node = await nodeRepository.GetNodeAsync(id);

            if (node == null)
            {
                return NotFound();
            }
            var model = mapper.Map<NodeVM>(node);
            return View(model);
        }

        // GET: Nodes/Create
        public async Task<IActionResult> CreateAsync()
        {
            var model = new NodeCreateVM
            {
                Station = new SelectList(await stationsRepository.GetAllAsync(), "Id", "Name")
            };
            return View(model);
        }

        // POST: Nodes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( NodeCreateVM nodeCreateVM)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var model = mapper.Map<Node>(nodeCreateVM);
                    //model.Station1 = await stationsRepository.GetAsync(model.Station1_Id);
                    //model.Station2 = await stationsRepository.GetAsync(model.Station2_Id);
                    //model.Name1 = model.Station1.Name;
                    //model.Name2 = model.Station2.Name;
                    //await graphNetworkRepository.AddAsync(model);
                    model.Station=await stationsRepository.GetAsync(model.StationId);
                    await nodeRepository.GenerateNeighboursAsync(model);
                    await nodeRepository.AddAsync(model);
                    return RedirectToAction(nameof(Index));
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An Error Has Occurred. Please Try Again Later");
            }

            nodeCreateVM.Station = new SelectList(await stationsRepository.GetAllAsync(), "Id", "Name", nodeCreateVM.StationId);
            


            return View(nodeCreateVM);
        }

        // GET: Nodes/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.Nodes == null)
        //    {
        //        return NotFound();
        //    }

        //    var node = await _context.Nodes.FindAsync(id);
        //    if (node == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["StationId"] = new SelectList(_context.Stations, "Id", "Id", node.StationId);
        //    return View(node);
        //}

        //// POST: Nodes/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,StationId,Neighbours")] Node node)
        //{
        //    if (id != node.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(node);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!NodeExists(node.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["StationId"] = new SelectList(_context.Stations, "Id", "Id", node.StationId);
        //    return View(node);
        //}

        // GET: Nodes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {


            var node = mapper.Map<NodeVM>( await nodeRepository.GetNodeAsync(id));
            if (node == null)
            {
                return NotFound();
            }
     
            return View(node);
        }

        // POST: Nodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var node = mapper.Map<NodeVM>(await nodeRepository.GetNodeAsync(id));
            if (node != null)
            {
                await nodeRepository.DeleteAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }

        //private bool NodeExists(int id)
        //{
        //  return (_context.Nodes?.Any(e => e.Id == id)).GetValueOrDefault();
        //}

        public async Task<IActionResult> Generate()
        {
            await nodeRepository.GenerateDatabaseAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
