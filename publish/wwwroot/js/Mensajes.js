// Animación de mensaje enviado
function mostrarMensaje(texto) {
    const div = document.getElementById('mensaje-enviado');
    if (!div) return; // ⚡ seguridad
    div.innerText = texto;
    div.style.display = 'block';
    setTimeout(() => {
        div.style.display = 'none';
    }, 2000);
}

// Función para enviar mensajes y actualizar todo
async function enviar(de, para, contenido) {
    if (!de || !para || !contenido) return;

    try {
        await fetch('/api/mensajes/enviar', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ De: de, Para: para, Contenido: contenido })
        });

        // Animación según remitente
        if (de === 'gordito') {
            mostrarMensaje('💌 Tu gordito te extraña');
        } else {
            mostrarMensaje('💌 Tu osita te extraña');
        }

        // Actualizar contadores e historial
        actualizarContadores();
        actualizarHistorial();
    } catch (err) {
        console.error('Error al enviar mensaje:', err);
    }
}

// Actualizar contadores
async function actualizarContadores() {
    const contadorHoyOsita = document.getElementById('hoyOsita');
    const contadorTotalOsita = document.getElementById('totalOsita');
    const contadorHoyGordito = document.getElementById('hoyGordito');
    const contadorTotalGordito = document.getElementById('totalGordito');

    try {
        const res = await fetch('/api/mensajes/contar');
        const data = await res.json();

        if (contadorHoyOsita) contadorHoyOsita.innerText = data.hoyOsita;
        if (contadorTotalOsita) contadorTotalOsita.innerText = data.totalOsita;
        if (contadorHoyGordito) contadorHoyGordito.innerText = data.hoyGordito;
        if (contadorTotalGordito) contadorTotalGordito.innerText = data.totalGordito;
    } catch (err) {
        console.error('Error al actualizar contadores:', err);
    }
}

// Actualizar historial
async function actualizarHistorial() {
    const ul = document.getElementById('lista-mensajes');
    if (!ul) return;

    try {
        const res = await fetch('/api/mensajes/todos');
        const mensajes = await res.json();
        ul.innerHTML = ''; // limpiar

        if (mensajes.length === 0) {
            const li = document.createElement('li');
            li.innerText = 'No hay mensajes todavía 💌';
            ul.appendChild(li);
            return;
        }

        mensajes.forEach(m => {
            const li = document.createElement('li');
            const fecha = new Date(m.createdAt).toLocaleString();
            li.innerText = `${m.de} → ${m.para}: ${m.contenido} 💌 ${fecha}`;
            ul.appendChild(li);
        });
    } catch (err) {
        console.error('Error al actualizar historial:', err);
    }
}

// Cargar contadores y historial al entrar
document.addEventListener("DOMContentLoaded", () => {
    actualizarContadores();
    actualizarHistorial();

    // Manejar envío si existe el formulario
    const btn = document.getElementById('enviar-mensaje');
    if (btn) {
        btn.addEventListener('click', () => {
            const autor = document.getElementById('autor')?.value.trim();
            const contenido = document.getElementById('contenido')?.value.trim();
            const para = document.getElementById('para')?.value.trim() || 'osita'; // default

            if (!autor || !contenido) return;

            enviar(autor, para, contenido);

            // Limpiar campos
            if (document.getElementById('autor')) document.getElementById('autor').value = '';
            if (document.getElementById('contenido')) document.getElementById('contenido').value = '';
        });
    }
});
