﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OhMyDogAPI.Model;
using OhMyDogAPI.Model.dto;
using OhMyDogAPI.Repository;

namespace OhMyDogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        UsuarioRepository _usuarioRepository;
        public UsuarioController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        [HttpGet]
        public IActionResult ListarUsuarios() 
        {
            try
            {
                return Ok(_usuarioRepository.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("buscar")]
        public IActionResult BuscarUsuario(int id, string? cpf = null) => id == 0 ? BuscarUsuarioPorCpf(cpf) : BuscarUsuarioPorId(id);

        private IActionResult BuscarUsuarioPorId(int id)
        {
            try
            {
                return Ok(_usuarioRepository.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private IActionResult BuscarUsuarioPorCpf(string cpf)
        {
            try
            {
                return Ok(_usuarioRepository.GetByCpf(cpf));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("cadastro")]
        public IActionResult Cadastrar(Usuario usuario)
        {
            return Ok(_usuarioRepository.Create(usuario));
        }

        [HttpPut]
        public IActionResult Atualizar (Usuario usuario)
        {
            return Ok(_usuarioRepository.Update(usuario));
        }

        [HttpPost("login")]
        public IActionResult Login(Credenciais credenciais)
        {
            try
            {
                var result = _usuarioRepository.Login(credenciais);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
