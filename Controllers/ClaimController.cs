using Microsoft.AspNetCore.Mvc;
using TestProject1.Data.Interfaces;
using TestProject1.Data.Models;

namespace TestProject1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClaimController : ControllerBase
    {
        private readonly IClaimRepository _claimRepository;

        public ClaimController(IClaimRepository claimRepository)
        {
            _claimRepository = claimRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Claim>>> Get()
        {
            var claims = await _claimRepository.GetClaims();
            return Ok(claims);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Claim>> Get(int id)
        {
            var claim = await _claimRepository.GetClaimById(id);
            if (claim == null)
            {
                return NotFound();
            }
            return Ok(claim);
        }

        [HttpPost]
        public async Task<ActionResult<Claim>> Post( Claim claim)
        {
            var createdClaim = await _claimRepository.Create(claim);
            return CreatedAtAction(nameof(Get), new { id = createdClaim.Id }, createdClaim);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Claim claim)
        {
            if (id != claim.Id)
            {
                return BadRequest();
            }

            var claimToUpdate = await _claimRepository.GetClaimById(id);
            if (claimToUpdate == null)
            {
                return NotFound();
            }

            claimToUpdate.Name = claim.Name;
            claimToUpdate.Message = claim.Message;

            var result = await _claimRepository.Update(claimToUpdate);
            if (!result)
            {
                return StatusCode(500);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _claimRepository.Delete(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}    

