<template>
  <div class="app1">
    <header style="display: flex; flex-direction: column">
      <div style="display: flex; flex-direction: row">
        <div>
          <router-link class="linka" to="/">
            <img src="./assets/img/logoWithText.png" />
          </router-link>
        </div>

        <nav class="desctop-element tablet-element">
          <router-link to="/rules">Site rules</router-link>
          <router-link to="/contacts">Contacts</router-link>
          <router-link to="/reviews">Reviews</router-link>
        </nav>
        <div class="nav-button-wrapper mobile-element">
          <button href="" class="nav-btn mobile-menu" id="collapse_navbar">
            <div class="mobile-burger-menu">
              <img class="burger-icon" src="./assets/img/burger-menu.png" />
            </div>
          </button>
        </div>
      </div>
      <div class="content" id="collapse_navbar_content">
        <nav class="mobile-menu-items">
          <router-link class="mobile-menu-item" to="/rules"
            >Site rules</router-link
          >
          <router-link class="mobile-menu-item" to="/contacts"
            >Contacts</router-link
          >
          <router-link class="mobile-menu-item" to="/reviews"
            >Reviews</router-link
          >
        </nav>
      </div>
    </header>

    <div class="app">
      <router-view />
    </div>

    <!-- Footer -->

    <footer>
      <a href="#main" class="footer-logo">
        <img src="./assets/img/logoPlanet.jpeg" />
        <div>
          <p class="footer-logo-title"><span>Global</span> exchange</p>
          <p class="footer-logo-desc">Safe deal - customer confidense</p>
        </div>
      </a>
      <p class="footer-info-text">
        © 2023 Globalexchange.com — electronic currency exchange service.
      </p>
    </footer>
    <!-- Footer -->
  </div>
</template>

<script>
import('./assets/js/collapse');
export default {};
</script>

<style>
@import url('./assets/css/style.css');

.app1 {
  min-height: 100vh;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
}
</style>
