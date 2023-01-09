<template>
  <div class="app1">
    <header>
      <div>
        <router-link class="linka" to="/">
          <img src="../src/assets/img/logoWithText.png"
        /></router-link>
      </div>

      <nav>
        <router-link class="linka" to="/rules">Site rules</router-link>
        <router-link class="linka" to="/contacts">Contacts</router-link>
        <router-link class="linka" to="/reviews">Reviews</router-link>
      </nav>
    </header>

    <div class="app">
      <router-view />
    </div>

    <!-- Footer -->

    <footer>
      <div>
        <a href="#main" class="footer-logo">
          <img src="../src/assets/img/logoPlanet.jpeg" />
          <div>
            <p class="footer-logo-title"><span>Global</span> exchange</p>
            <p class="footer-logo-desc">Safe deal - customer confidense</p>
          </div>
        </a>
        <p class="footer-info-text margin-top-50">
          © 2018-2022 Globalexchange.com — electronic currency exchange service.
        </p>
      </div>

      <div>
        <a href="" class="footer-link">Popular exchange directions</a>
      </div>

      <div>
        <a href="" class="footer-link">Menu</a>
      </div>

      <div>
        <a href="" class="footer-link">Operating mode</a>
        <p class="footer-info-text margin-top-20">
          Opening hours: 09:00 to 24:00 GMT+3 daily
        </p>
      </div>
    </footer>
    <!-- Footer -->
  </div>
</template>

<style>
@import url('./assets/css/style.css');

.app1 {
  min-height: 100vh;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
}

/* .linka {
  text-decoration: none;
} */
</style>
