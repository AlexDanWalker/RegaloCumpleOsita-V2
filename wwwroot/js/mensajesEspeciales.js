document.addEventListener("DOMContentLoaded", () => {
    const listaMensajes = document.getElementById("lista-mensajes");
    const formulario = document.getElementById("formulario-mensaje");
    const inputAutor = document.getElementById("autor");
    const inputContenido = document.getElementById("contenido");

    // Div para animación de mensaje enviado
    let mensajeAnimado = document.createElement("div");
    mensajeAnimado.id = "mensaje-enviado";
    mensajeAnimado.style.display = "none";
    mensajeAnimado.style.position = "fixed";
    mensajeAnimado.style.top = "10%";
    mensajeAnimado.style.left = "50%";
    mensajeAnimado.style.transform = "translateX(-50%)";
    mensajeAnimado.style.padding = "15px 25px";
    mensajeAnimado.style.backgroundColor = "#ff80c0";
    mensajeAnimado.style.color = "white";
    mensajeAnimado.style.borderRadius = "12px";
    mensajeAnimado.style.boxShadow = "0 4px 12px rgba(0,0,0,0.2)";
    mensajeAnimado.style.fontSize = "1.1em";
    mensajeAnimado.style.zIndex = "9999";
    document.body.appendChild(mensajeAnimado);

    // URL del API
    const apiUrl = '/api/MensajesEspecialesApi';

    // Función para mostrar mensaje animado
    function mostrarMensaje(texto) {
        mensajeAnimado.innerText = texto;
        mensajeAnimado.style.display = "block";
        setTimeout(() => mensajeAnimado.style.display = "none", 2000);
    }

    // Función para cargar mensajes
    async function cargarMensajes() {
        if (!listaMensajes) return;
        try {
            const res = await fetch(`${apiUrl}/todos`); // ⚡ aquí está el cambio
            if (!res.ok) throw new Error(`HTTP error! status: ${res.status}`);
            const mensajes = await res.json();
            listaMensajes.innerHTML = '';

            if (!mensajes || mensajes.length === 0) {
                listaMensajes.innerHTML = `<p>No hay mensajes todavía 💌</p>`;
            } else {
                mensajes.forEach(m => {
                    const li = document.createElement("li");
                    li.className = "mensaje-especial";
                    li.textContent = `${m.autor}: ${m.contenido}`;
                    listaMensajes.appendChild(li);
                });
            }
        } catch (err) {
            console.error("Error cargando mensajes:", err);
            listaMensajes.innerHTML = `<p>Error al cargar mensajes 💔</p>`;
        }
    }

    // Función para enviar mensaje
    async function enviarMensaje() {
        if (!formulario) return;

        const autor = inputAutor.value.trim();
        const contenido = inputContenido.value.trim();

        if (!autor || !contenido) {
            alert("Por favor completa todos los campos 💌");
            return;
        }

        const mensaje = { autor, contenido };

        try {
            const res = await fetch(`${apiUrl}/enviar`, { // ⚡ ruta correcta
                method: "POST",
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(mensaje)
            });
            if (!res.ok) throw new Error(`HTTP error! status: ${res.status}`);
            const data = await res.json();

            if (data.success) {
                inputContenido.value = '';
                mostrarMensaje("Mensaje enviado 💖");
                cargarMensajes();
            } else {
                alert("Error al enviar mensaje 💔");
            }
        } catch (err) {
            console.error("Error al enviar mensaje:", err);
            alert("Error al enviar mensaje 💔");
        }
    }

    // Listener para formulario
    if (formulario) {
        formulario.addEventListener("submit", (e) => {
            e.preventDefault();
            enviarMensaje();
        });
    }

    // Cargar mensajes al inicio
    cargarMensajes();
});
