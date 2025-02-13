(function () {
  //===== Prealoder

  window.onload = function () {
      window.setTimeout(fadeout, 500);
  };

  function fadeout() {
    document.querySelector(".preloader").style.opacity = "0";
    document.querySelector(".preloader").style.display = "none";
  }

  /*=====================================
    Sticky
    ======================================= */
  window.onscroll = function () {
    const header_navbar = document.querySelector(".navbar-area");
    const sticky = header_navbar.offsetTop;
    const logo = document.querySelector(".navbar-brand img");

    if (window.pageYOffset > sticky) {
      header_navbar.classList.add("sticky");
      logo.src = "assets/img/logo/32x32.png";
    } else {
      header_navbar.classList.remove("sticky");
      logo.src = "assets/img/logo/32x32.png";
    }

    // show or hide the back-top-top button
    const backToTo = document.querySelector(".scroll-top");
    if (
      document.body.scrollTop > 50 ||
      document.documentElement.scrollTop > 50
    ) {
      backToTo.style.display = "flex";
    } else {
      backToTo.style.display = "none";
    }
  };

  // for menu scroll
  const pageLink = document.querySelectorAll(".page-scroll");

  pageLink.forEach((elem) => {
    elem.addEventListener("click", (e) => {
      e.preventDefault();
      document.querySelector(elem.getAttribute("href")).scrollIntoView({
        behavior: "smooth",
        offsetTop: 1 - 60,
      });
    });
  });

  // section menu active
  function onScroll(event) {
    const sections = document.querySelectorAll(".page-scroll");
    const scrollPos =
      window.pageYOffset ||
      document.documentElement.scrollTop ||
      document.body.scrollTop;

    for (let i = 0; i < sections.length; i++) {
      const currLink = sections[i];
      const val = currLink.getAttribute("href");
      const refElement = document.querySelector(val);
      const scrollTopMinus = scrollPos + 73;
      if (
        refElement.offsetTop <= scrollTopMinus &&
        refElement.offsetTop + refElement.offsetHeight > scrollTopMinus
      ) {
        document.querySelector(".page-scroll").classList.remove("active");
        currLink.classList.add("active");
      } else {
        currLink.classList.remove("active");
      }
    }
  }

  window.document.addEventListener("scroll", onScroll);

  //===== close navbar-collapse when a  clicked
  let navbarToggler = document.querySelector(".navbar-toggler");
  const navbarCollapse = document.querySelector(".navbar-collapse");

  document.querySelectorAll(".page-scroll").forEach((e) =>
    e.addEventListener("click", () => {
      navbarToggler.classList.remove("active");
      navbarCollapse.classList.remove("show");
    })
  );
  navbarToggler.addEventListener("click", function () {
    navbarToggler.classList.toggle("active");
  });

    var validate = function (token) {
        fetch('https://api.moonbotapp.com/download?id=' + token, { 
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.text();
            })
            .then(url => {
                if (url) {
                    window.open(url, '_blank');
                } else {
                    alert('URL was not provided in the response.');
                }
            })
            .catch(error => console.error('Error:', error))
            .finally(() => {
                document.getElementById('recaptcha-container').style.display = 'none';
                grecaptcha.reset();

                var loadingButton = document.getElementById('button__loader');
                var downloadButton = document.getElementById('download-btn');
                loadingButton.classList.add('button__loader--hide');
                downloadButton.classList.remove('button--loading');
                downloadButton.disabled = false;
                downloadButton.querySelector('span').innerHTML = 'Download free version';
            })
    };

    document.getElementById('download-btn').addEventListener('click', function () {
        var loadingButton = document.getElementById('button__loader');
        var downloadButton = document.getElementById('download-btn');
        loadingButton.classList.remove('button__loader--hide');
        downloadButton.classList.add('button--loading');
        downloadButton.disabled = true;
        downloadButton.querySelector('span').innerHTML = 'Loading...';

        var recaptchaContainer = document.getElementById('recaptcha-container');
        recaptchaContainer.style.display = 'block';

        grecaptcha.render(recaptchaContainer, {
            sitekey: '6LfTVd0oAAAAAGWSpSQF5uCKbhuzHQ67bZIdI9ob',
            theme: 'light',
            callback: validate
        });

    });


  // WOW active
  new WOW().init();
})();
