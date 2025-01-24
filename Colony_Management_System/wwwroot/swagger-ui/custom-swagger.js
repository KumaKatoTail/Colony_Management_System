window.onload = function () {
    const loginButton = document.querySelector('button[data-sw-qa-id="authorize"]');

    if (loginButton) {
        loginButton.addEventListener('click', async function () {
            const email = 'your-email@example.com'; // Zamień na email użytkownika
            const password = 'your-password'; // Zamień na hasło użytkownika

            // Wywołaj endpoint logowania, aby uzyskać token
            const response = await fetch('/api/auth/Login', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    Email: email,
                    Haslo: password,
                }),
            });

            if (response.ok) {
                const data = await response.json();
                const token = data.token;

                // Rozpakuj token JWT, aby wyciągnąć UprId
                const decodedToken = JSON.parse(atob(token.split('.')[1]));
                const uprId = decodedToken.UprId; // Odczytaj UprId z tokenu

                console.log('UprId:', uprId); // Możesz użyć UprId w dowolny sposób

                // Ustaw token w Swaggerze
                const swaggerUi = window.ui;
                swaggerUi.preauthorizeApiKey('Bearer', token);

                // Zamknij okno logowania w Swagger UI
                const modal = document.querySelector('.swagger-ui .modal');
                if (modal) {
                    modal.style.display = 'none';
                }
            } else {
                alert('Login failed');
            }
        });
    }
};
