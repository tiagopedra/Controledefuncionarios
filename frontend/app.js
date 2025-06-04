// app.js profissional

document.addEventListener('DOMContentLoaded', function () {
    // Dados simulados
    let funcionarios = [];
    let cargos = [];
    let setores = [];
    let dadosBancariosList = [];
    let enderecosList = [];
    let editandoFuncionarioId = null;
    const content = document.getElementById('content');

    document.getElementById('view-funcionarios').addEventListener('click', carregarFuncionarios);
    document.getElementById('view-cargos').addEventListener('click', async () => {
        await carregarCargos();
        showCargos();
    });
    document.getElementById('view-setores').addEventListener('click', async () => {
        await carregarSetores();
        showSetores();
    });
    // Exibir Funcionários por padrão
    Promise.all([carregarCargos(), carregarSetores(), carregarDadosBancarios(), carregarEnderecos()]).then(carregarFuncionarios);

    // Funções para carregar dados bancários e endereços
    async function carregarDadosBancarios() {
        try {
            const resp = await fetch('http://localhost:5077/api/dadosbancarios');
            if (!resp.ok) throw new Error('Erro ao buscar dados bancários');
            dadosBancariosList = await resp.json();
        } catch (e) {
            mostrarAlerta('Erro ao carregar dados bancários: ' + e.message, 'danger');
        }
    }
    async function carregarEnderecos() {
        try {
            const resp = await fetch('http://localhost:5077/api/endereco');
            if (!resp.ok) throw new Error('Erro ao buscar endereços');
            enderecosList = await resp.json();
        } catch (e) {
            mostrarAlerta('Erro ao carregar endereços: ' + e.message, 'danger');
        }
    }

    // Funções de renderização
    async function showFuncionarios() {
    console.log('Funcionários renderizados:', funcionarios);
    let funcionariosFiltrados = funcionarios;
        content.innerHTML = `
            <div class="d-flex justify-content-between align-items-center mb-3">
                <h2 class="mb-0">Funcionários</h2>
                <div class="input-group w-auto ms-3" style="max-width:300px;display:inline-flex">
  <input id="search-funcionario" class="form-control" placeholder="Pesquisar funcionário...">
  <button id="btn-search-funcionario" class="btn btn-outline-secondary" type="button"><i class="bi bi-search"></i></button>
</div>
                <button id="add-funcionario" class="btn btn-success"><i class="bi bi-plus-circle"></i> Novo Funcionário</button>
            </div>
            <div id="alert-area"></div>
            <div class="table-responsive">
                <table class="table table-striped align-middle">
                    <thead class="table-dark">
                        <tr><th>Nome</th><th>Cargo</th><th>Setor</th><th class="text-end">Ações</th></tr>
                    </thead>
                    <tbody>
                        ${funcionariosFiltrados.map(f => {
                        console.log('Renderizando funcionário:', f);
                        return `
                        <tr>
                            <td>
                                <div><strong>${f.pessoa?.nome || ''}</strong></div>
                                
                            </td>
                            <td>
                                <div>${f.cargo?.nome || '<span style="color:red">Sem cargo</span>'}</div>
                                
                            </td>
                            <td>
                                <div>${f.setor?.nome || '<span style="color:red">Sem setor</span>'}</div>
                                
                            </td>
                            <td class="text-end">
                                <button class="btn btn-primary btn-sm me-1" data-edit="${f.id}"><i class="bi bi-pencil"></i></button>
                                <button class="btn btn-danger btn-sm" data-delete="${f.id}"><i class="bi bi-trash"></i></button>
                            </td>
                        </tr>
                        `;
                    }).join('')}
                    </tbody>
                </table>
            </div>
        `;
        document.getElementById('add-funcionario').onclick = () => abrirModalFuncionario();
        function filtrarFuncionarios() {
    const termo = document.getElementById('search-funcionario').value.toLowerCase();
    const filtrados = funcionarios.filter(f =>
        (f.pessoa?.nome || '').toLowerCase().includes(termo) ||
        (f.cargo?.nome || '').toLowerCase().includes(termo) ||
        (f.setor?.nome || '').toLowerCase().includes(termo)
    );
    const tbody = document.querySelector('tbody');
tbody.innerHTML = filtrados.map(f => `
<tr>
    <td><div><strong>${f.pessoa?.nome || ''}</strong></div></td>
    <td><div>${f.cargo?.nome || '<span style=\"color:red\">Sem cargo</span>'}</div></td>
    <td><div>${f.setor?.nome || '<span style=\"color:red\">Sem setor</span>'}</div></td>
    <td class="text-end">
        <button class="btn btn-primary btn-sm me-1" data-edit="${f.id}"><i class="bi bi-pencil"></i></button>
        <button class="btn btn-danger btn-sm" data-delete="${f.id}"><i class="bi bi-trash"></i></button>
    </td>
</tr>
`).join('');
// Reaplicar eventos
tbody.querySelectorAll('[data-edit]').forEach(btn => {
    btn.onclick = () => abrirModalFuncionario(parseInt(btn.getAttribute('data-edit')));
});
tbody.querySelectorAll('[data-delete]').forEach(btn => {
    btn.onclick = () => deletarFuncionario(parseInt(btn.getAttribute('data-delete')));
});
}
document.getElementById('search-funcionario').addEventListener('input', filtrarFuncionarios);
document.getElementById('btn-search-funcionario').addEventListener('click', filtrarFuncionarios);
        document.querySelectorAll('[data-edit]').forEach(btn => {
            btn.onclick = () => abrirModalFuncionario(parseInt(btn.getAttribute('data-edit')));
        });
        document.querySelectorAll('[data-delete]').forEach(btn => {
            btn.onclick = () => deletarFuncionario(parseInt(btn.getAttribute('data-delete')));
        });
    }

    async function carregarFuncionarios() {
        try {
            const resp = await fetch('http://localhost:5077/api/funcionario');
            if (!resp.ok) throw new Error('Erro ao buscar funcionários');
            funcionarios = await resp.json();
            showFuncionarios();
        } catch (e) {
            mostrarAlerta('Erro ao carregar funcionários: ' + e.message, 'danger');
        }
    }

    async function carregarCargos() {
        try {
            const resp = await fetch('http://localhost:5077/api/cargo');
            if (!resp.ok) throw new Error('Erro ao buscar cargos');
            cargos = await resp.json();
        } catch (e) {
            mostrarAlerta('Erro ao carregar cargos: ' + e.message, 'danger');
        }
    }

    async function carregarSetores() {
        try {
            const resp = await fetch('http://localhost:5077/api/setor');
            if (!resp.ok) throw new Error('Erro ao buscar setores');
            setores = await resp.json();
        } catch (e) {
            mostrarAlerta('Erro ao carregar setores: ' + e.message, 'danger');
        }
    }

    // Modal de cadastro/edição
    function abrirModalFuncionario(id) {
        const modal = new bootstrap.Modal(document.getElementById('mainModal'));
        const funcionario = id ? funcionarios.find(f => f.id === id) : { pessoa: { nome: '' }, cargo: cargos[0] || {}, setor: setores[0] || {} };
        editandoFuncionarioId = id || null;
        document.getElementById('mainModalLabel').innerText = id ? 'Editar Funcionário' : 'Novo Funcionário';
        document.getElementById('modalBody').innerHTML = `
            <form id="form-funcionario">
                ${id ? `<div class='mb-3'><label class='form-label'>ID Funcionário</label><input type='text' class='form-control' value='${funcionario.id}' readonly></div>` : ''} 
                <div class="mb-3">
                    <label class="form-label">Nome</label>
                    <input type="text" class="form-control" id="nome" value="${funcionario.pessoa?.nome || ''}" required>
                </div>
                <div class="mb-3">
                    <label class="form-label">CPF</label>
                    <input type="text" class="form-control" id="cpf" value="${funcionario.pessoa?.cpf || ''}" required>
                </div>
                <div class="mb-3">
                    <label class="form-label">Sexo</label>
                    <select class="form-select" id="sexo" required>
                      <option value="">Selecione...</option>
                      <option value="Masculino"${funcionario.pessoa?.sexo === 'Masculino' ? ' selected' : ''}>Masculino</option>
                      <option value="Feminino"${funcionario.pessoa?.sexo === 'Feminino' ? ' selected' : ''}>Feminino</option>
                      <option value="Outro"${funcionario.pessoa?.sexo === 'Outro' ? ' selected' : ''}>Outro</option>
                    </select>
                </div>
                <div class="mb-3">
                    <label class="form-label">Telefone</label>
                    <input type="text" class="form-control" id="telefone" value="${funcionario.pessoa?.telefone || ''}" required>
                </div>
                <div class="mb-3">
                    <label class="form-label">Email</label>
                    <input type="email" class="form-control" id="email" value="${funcionario.pessoa?.email || ''}" required>
                </div>
                <div class="mb-3">
                    <label class="form-label">Cargo</label>
                    <select class="form-select" id="cargo">
                        ${cargos.map(c => `<option value="${c.id}"${funcionario.cargo?.id === c.id ? ' selected' : ''}>${c.nome}</option>`).join('')}
                    </select>
                </div>
                <div class="mb-3">
                    <label class="form-label">Setor</label>
                    <select class="form-select" id="setor">
                        ${setores.map(s => `<option value="${s.id}"${funcionario.setor?.id === s.id ? ' selected' : ''}>${s.nome}</option>`).join('')}
                    </select>
                </div>
                <div class="mb-3">
                    <label class="form-label">Dados Bancários</label>
                    <select class="form-select" id="dadosBancariosId" required>
                        <option value="">Selecione...</option>
                        ${dadosBancariosList.map(db => `<option value="${db.id}"${funcionario.dadosBancariosId === db.id ? ' selected' : ''}>${db.banco} - ${db.agencia}/${db.conta}</option>`).join('')}
                    </select>
                </div>
                <div class="mb-3">
                    <label class="form-label">Endereço</label>
                    <select class="form-select" id="enderecoID" required>
                        <option value="">Selecione...</option>
                        ${enderecosList.map(e => `<option value="${e.id}"${funcionario.enderecoID === e.id ? ' selected' : ''}>${[e.logradouro, e.numero, e.cidade].filter(x => !!x).join(', ')}</option>`).join('')}

                    </select>
                </div>
            </form>
        `;
        document.getElementById('modalFooter').innerHTML = `
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
            <button type="button" class="btn btn-success" id="salvarFuncionario">Salvar</button>
        `;
        document.getElementById('salvarFuncionario').onclick = salvarFuncionario;
        modal.show();
    }

    async function salvarFuncionario() {
        const nome = document.getElementById('nome').value.trim();
        const cpf = document.getElementById('cpf').value.trim();
        const sexo = document.getElementById('sexo').value;
        const telefone = document.getElementById('telefone').value.trim();
        const email = document.getElementById('email').value.trim();
        const cargoId = parseInt(document.getElementById('cargo').value);
        const setorId = parseInt(document.getElementById('setor').value);
        const dadosBancariosId = parseInt(document.getElementById('dadosBancariosId').value);
        const enderecoID = parseInt(document.getElementById('enderecoID').value);
        if (!nome || !cpf || !sexo || !telefone || !email || isNaN(cargoId) || isNaN(setorId) || isNaN(dadosBancariosId) || isNaN(enderecoID) || cargoId <= 0 || setorId <= 0 || dadosBancariosId <= 0 || enderecoID <= 0) {
            mostrarAlerta('Preencha todos os campos obrigatórios corretamente.', 'danger');
            return;
        }
        const cargo = cargos.find(c => c.id === cargoId);
        const setor = setores.find(s => s.id === setorId);
        const dadosBancarios = dadosBancariosList.find(db => db.id === dadosBancariosId);
        const endereco = enderecosList.find(e => e.id === enderecoID);
        
        const body = JSON.stringify({ pessoa: { nome, cpf, sexo, telefone, email }, cargoId, setorId, dadosBancariosId, enderecoID });
        console.log('Enviando para o backend:', { pessoa: { nome, cpf, sexo, telefone, email }, cargoId, setorId, dadosBancariosId, enderecoID });
        try {
            if (editandoFuncionarioId) {
                
                const resp = await fetch(`http://localhost:5077/api/funcionario/${editandoFuncionarioId}`, {
                    method: 'PUT',
                    headers: { 'Content-Type': 'application/json' },
                    body
                });
                const respText = await resp.text();
                console.log('Resposta do backend (PUT):', resp.status, respText);
                if (!resp.ok) throw new Error('Erro ao cadastrar funcionário');
                mostrarAlerta('Funcionário cadastrado com sucesso!', 'success');
            } else {
                const resp = await fetch('http://localhost:5077/api/funcionario', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body
                });
                const respText = await resp.text();
                console.log('Resposta do backend (POST):', resp.status, respText);
                if (!resp.ok) throw new Error('Erro ao cadastrar funcionário');
                mostrarAlerta('Funcionário cadastrado com sucesso!', 'success');
            }
            bootstrap.Modal.getInstance(document.getElementById('mainModal')).hide();
            carregarFuncionarios();
        } catch (e) {
            mostrarAlerta('Erro ao salvar funcionário: ' + e.message, 'danger');
        }
    }

    async function deletarFuncionario(id) {
        if (confirm('Deseja realmente excluir este funcionário?')) {
            try {
                const resp = await fetch(`http://localhost:5077/api/funcionario/${id}`, {
                    method: 'DELETE'
                });
                if (!resp.ok) throw new Error('Erro ao excluir funcionário');
                mostrarAlerta('Funcionário excluído.', 'warning');
                carregarFuncionarios();
            } catch (e) {
                mostrarAlerta('Erro ao excluir funcionário: ' + e.message, 'danger');
            }
        }
    }

    function mostrarAlerta(msg, tipo) {
        document.getElementById('alert-area').innerHTML = `<div class="alert alert-${tipo} alert-dismissible fade show" role="alert">
            ${msg}
            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
        </div>`;
    }

    
    function showCargos() {
        let cargosFiltrados = cargos;
        content.innerHTML = `
            <div class="d-flex justify-content-between align-items-center mb-3">
                <h2 class="mb-0">Cargos</h2>
                <div class="input-group w-auto ms-3" style="max-width:300px;display:inline-flex">
  <input id="search-cargo" class="form-control" placeholder="Pesquisar cargo...">
  <button id="btn-search-cargo" class="btn btn-outline-secondary" type="button"><i class="bi bi-search"></i></button>
</div>
                <button id="add-cargo" class="btn btn-success"><i class="bi bi-plus-circle"></i> Novo Cargo</button>
            </div>
            <div id="alert-area"></div>
            <ul class="list-group">
                ${cargosFiltrados.map(c => `<li class="list-group-item d-flex justify-content-between align-items-center">
                    <span>${c.nome}</span>
                    <span>
                        <button class="btn btn-primary btn-sm me-1" data-edit-cargo="${c.id}"><i class="bi bi-pencil"></i></button>
                        <button class="btn btn-danger btn-sm" data-delete-cargo="${c.id}"><i class="bi bi-trash"></i></button>
                    </span>
                </li>`).join('')}
            </ul>
        `;
        document.getElementById('add-cargo').onclick = () => abrirModalCargo();
        function filtrarCargos() {
    const termo = document.getElementById('search-cargo').value.toLowerCase();
    const filtrados = cargos.filter(c =>
        (c.nome || '').toLowerCase().includes(termo)
    );
    const ul = document.querySelector('.list-group');
    ul.innerHTML = filtrados.map(c => `<li class="list-group-item d-flex justify-content-between align-items-center">
        <span>${c.nome}</span>
        <span>
            <button class="btn btn-primary btn-sm me-1" data-edit-cargo="${c.id}"><i class="bi bi-pencil"></i></button>
            <button class="btn btn-danger btn-sm" data-delete-cargo="${c.id}"><i class="bi bi-trash"></i></button>
        </span>
    </li>`).join('');
    ul.querySelectorAll('[data-edit-cargo]').forEach(btn => {
        btn.onclick = () => abrirModalCargo(parseInt(btn.getAttribute('data-edit-cargo')));
    });
    ul.querySelectorAll('[data-delete-cargo]').forEach(btn => {
        btn.onclick = () => deletarCargo(parseInt(btn.getAttribute('data-delete-cargo')));
    });
}
document.getElementById('search-cargo').addEventListener('input', filtrarCargos);
document.getElementById('btn-search-cargo').addEventListener('click', filtrarCargos);
        document.querySelectorAll('[data-edit-cargo]').forEach(btn => {
            btn.onclick = () => abrirModalCargo(parseInt(btn.getAttribute('data-edit-cargo')));
        });
        document.querySelectorAll('[data-delete-cargo]').forEach(btn => {
            btn.onclick = () => deletarCargo(parseInt(btn.getAttribute('data-delete-cargo')));
        });
    }

    function abrirModalCargo(id) {
        const modal = new bootstrap.Modal(document.getElementById('mainModal'));
        const cargo = id ? cargos.find(c => c.id === id) : { nome: '' };
        document.getElementById('mainModalLabel').innerText = id ? 'Editar Cargo' : 'Novo Cargo';
        document.getElementById('modalBody').innerHTML = `
            <form id="form-cargo">
                <div class="mb-3">
                    <label class="form-label">Nome do Cargo</label>
                    <input type="text" class="form-control" id="nomeCargo" value="${cargo.nome || ''}" required>
                </div>
            </form>
        `;
        document.getElementById('modalFooter').innerHTML = `
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
            <button type="button" class="btn btn-success" id="salvarCargo">Salvar</button>
        `;
        document.getElementById('salvarCargo').onclick = () => salvarCargo(id);
        modal.show();
    }

    async function salvarCargo(id) {
        const nome = document.getElementById('nomeCargo').value.trim();
        if (!nome) {
            mostrarAlerta('Preencha o nome do cargo.', 'danger');
            return;
        }
        const body = JSON.stringify({ nome });
        try {
            if (id) {
                const resp = await fetch(`http://localhost:5077/api/cargo/${id}`, {
                    method: 'PUT',
                    headers: { 'Content-Type': 'application/json' },
                    body
                });
                if (!resp.ok) throw new Error('Erro ao atualizar cargo');
                mostrarAlerta('Cargo atualizado com sucesso!', 'success');
            } else {
                const resp = await fetch('http://localhost:5077/api/cargo', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body
                });
                if (!resp.ok) throw new Error('Erro ao cadastrar cargo');
                mostrarAlerta('Cargo cadastrado com sucesso!', 'success');
            }
            bootstrap.Modal.getInstance(document.getElementById('mainModal')).hide();
            await carregarCargos();
             
            showCargos();
        } catch (e) {
            mostrarAlerta('Erro ao salvar cargo: ' + e.message, 'danger');
        }
    }

    async function deletarCargo(id) {
        if (confirm('Deseja realmente excluir este cargo?')) {
            try {
                const resp = await fetch(`http://localhost:5077/api/cargo/${id}`, {
                    method: 'DELETE'
                });
                if (!resp.ok) throw new Error('Erro ao excluir cargo');
                mostrarAlerta('Cargo excluído.', 'warning');
                await carregarCargos();
                 
                showCargos();
            } catch (e) {
                mostrarAlerta('Erro ao excluir cargo: ' + e.message, 'danger');
            }
        }
    }

    function showSetores() {
        let setoresFiltrados = setores;
        content.innerHTML = `
            <div class="d-flex justify-content-between align-items-center mb-3">
                <h2 class="mb-0">Setores</h2>
                <div class="input-group w-auto ms-3" style="max-width:300px;display:inline-flex">
  <input id="search-setor" class="form-control" placeholder="Pesquisar setor...">
  <button id="btn-search-setor" class="btn btn-outline-secondary" type="button"><i class="bi bi-search"></i></button>
</div>
                <button id="add-setor" class="btn btn-success"><i class="bi bi-plus-circle"></i> Novo Setor</button>
            </div>
            <div id="alert-area"></div>
            <ul class="list-group">
                ${setoresFiltrados.map(s => `<li class="list-group-item d-flex justify-content-between align-items-center">
                    <span>${s.nome}</span>
                    <span>
                        <button class="btn btn-primary btn-sm me-1" data-edit-setor="${s.id}"><i class="bi bi-pencil"></i></button>
                        <button class="btn btn-danger btn-sm" data-delete-setor="${s.id}"><i class="bi bi-trash"></i></button>
                    </span>
                </li>`).join('')}
            </ul>
        `;
        document.getElementById('add-setor').onclick = () => abrirModalSetor();
        function filtrarSetores() {
    const termo = document.getElementById('search-setor').value.toLowerCase();
    const filtrados = setores.filter(s =>
        (s.nome || '').toLowerCase().includes(termo)
    );
    const ul = document.querySelector('.list-group');
    ul.innerHTML = filtrados.map(s => `<li class="list-group-item d-flex justify-content-between align-items-center">
        <span>${s.nome}</span>
        <span>
            <button class="btn btn-primary btn-sm me-1" data-edit-setor="${s.id}"><i class="bi bi-pencil"></i></button>
            <button class="btn btn-danger btn-sm" data-delete-setor="${s.id}"><i class="bi bi-trash"></i></button>
        </span>
    </li>`).join('');
    ul.querySelectorAll('[data-edit-setor]').forEach(btn => {
        btn.onclick = () => abrirModalSetor(parseInt(btn.getAttribute('data-edit-setor')));
    });
    ul.querySelectorAll('[data-delete-setor]').forEach(btn => {
        btn.onclick = () => deletarSetor(parseInt(btn.getAttribute('data-delete-setor')));
    });
}
document.getElementById('search-setor').addEventListener('input', filtrarSetores);
document.getElementById('btn-search-setor').addEventListener('click', filtrarSetores);
        document.querySelectorAll('[data-edit-setor]').forEach(btn => {
            btn.onclick = () => abrirModalSetor(parseInt(btn.getAttribute('data-edit-setor')));
        });
        document.querySelectorAll('[data-delete-setor]').forEach(btn => {
            btn.onclick = () => deletarSetor(parseInt(btn.getAttribute('data-delete-setor')));
        });
    }

    function abrirModalSetor(id) {
        const modal = new bootstrap.Modal(document.getElementById('mainModal'));
        const setor = id ? setores.find(s => s.id === id) : { nome: '' };
        document.getElementById('mainModalLabel').innerText = id ? 'Editar Setor' : 'Novo Setor';
        document.getElementById('modalBody').innerHTML = `
            <form id="form-setor">
                <div class="mb-3">
                    <label class="form-label">Nome do Setor</label>
                    <input type="text" class="form-control" id="nomeSetor" value="${setor.nome || ''}" required>
                </div>
            </form>
        `;
        document.getElementById('modalFooter').innerHTML = `
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
            <button type="button" class="btn btn-success" id="salvarSetor">Salvar</button>
        `;
        document.getElementById('salvarSetor').onclick = () => salvarSetor(id);
        modal.show();
    }

    // Agora as funções ficam no escopo principal:
    async function salvarSetor(id) {
        const nome = document.getElementById('nomeSetor').value.trim();
        if (!nome) {
            mostrarAlerta('Preencha o nome do setor.', 'danger');
            return;
        }
        const body = JSON.stringify({ nome, descricao: nome });
        try {
            if (id) {
                const resp = await fetch(`http://localhost:5077/api/setor/${id}`, {
                    method: 'PUT',
                    headers: { 'Content-Type': 'application/json' },
                    body
                });
                if (!resp.ok) throw new Error('Erro ao atualizar setor');
                mostrarAlerta('Setor atualizado com sucesso!', 'success');
            } else {
                const resp = await fetch('http://localhost:5077/api/setor', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body
                });
                if (!resp.ok) throw new Error('Erro ao cadastrar setor');
                mostrarAlerta('Setor cadastrado com sucesso!', 'success');
            }
            bootstrap.Modal.getInstance(document.getElementById('mainModal')).hide();
            await carregarSetores();
            showSetores();
        } catch (e) {
            mostrarAlerta('Erro ao salvar setor: ' + e.message, 'danger');
        }
    }

    async function deletarSetor(id) {
        if (confirm('Deseja realmente excluir este setor?')) {
            try {
                const resp = await fetch(`http://localhost:5077/api/setor/${id}`, {
                    method: 'DELETE'
                });
                if (!resp.ok) throw new Error('Erro ao excluir setor');
                mostrarAlerta('Setor excluído.', 'warning');
                await carregarSetores();
                 
                showSetores();
            } catch (e) {
                mostrarAlerta('Erro ao excluir setor: ' + e.message, 'danger');
            }
        }
    }

    async function deletarSetor(id) {
        if (confirm('Deseja realmente excluir este setor?')) {
            try {
                const resp = await fetch(`http://localhost:5077/api/setor/${id}`, {
                    method: 'DELETE'
                });
                if (!resp.ok) throw new Error('Erro ao excluir setor');
                mostrarAlerta('Setor excluído.', 'warning');
                await carregarSetores();
                 
                showSetores();
            } catch (e) {
                mostrarAlerta('Erro ao excluir setor: ' + e.message, 'danger');
            }
        }
    }

});
