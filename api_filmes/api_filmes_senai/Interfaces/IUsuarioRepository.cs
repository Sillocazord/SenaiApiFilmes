﻿using api_filmes_senai.Domains;

namespace api_filmes_senai.Interfaces
{
    public interface IUsuarioRepository
    {
        void Cadastrar(Usuario novoUsuario);

        Usuario BuscarPorID(Guid id);

        Usuario BuscarPorEmailESenha(string email, string senha);
    }
}
