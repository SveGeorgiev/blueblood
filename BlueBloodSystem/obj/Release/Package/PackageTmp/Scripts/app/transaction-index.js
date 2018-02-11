(function () {
    (function askCheckPassword() {
        if ((sessionStorage.getItem('isAuthenticated') === null || sessionStorage.getItem('isAuthenticated') === "false")
            && (sessionStorage.getItem('isAdmin') === null || sessionStorage.getItem('isAdmin') === "false")) {
            var password = prompt("Моля въвдете парола:", "");
            if (password !== null && password === "8814") {
                sessionStorage.setItem('isAuthenticated', true);
                document.getElementById("transactions-wrapper").style.display = 'block';
            } else if (password !== null && password === "240514") {
                sessionStorage.setItem('isAdmin', true);
                document.getElementById("transactions-wrapper").style.display = 'block';
                var adminElements = document.getElementsByClassName("admin");
                for (var i = 0; i < adminElements.length; i++) {
                    adminElements[i].style.display = 'inline-block';
                }
            }
            else {
                sessionStorage.setItem('isAuthenticated', false);
                sessionStorage.setItem('isAdmin', false);
                document.getElementById("transactions-wrapper").style.display = 'none';
                var adminElements = document.getElementsByClassName("admin");
                for (var i = 0; i < adminElements.length; i++) {
                    adminElements[i].style.display = 'none'
                }
            }
        } else {
            document.getElementById("transactions-wrapper").style.display = 'block';
            if (sessionStorage.getItem('isAdmin') !== null && sessionStorage.getItem('isAdmin') === "true") {
                var adminElements = document.getElementsByClassName("admin");
                for (var i = 0; i < adminElements.length; i++) {
                    adminElements[i].style.display = 'inline-block';
                }
            }
        }
    })();
})();