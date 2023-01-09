<template>
  <section class="welcome-section">
    <!-- <div class="chat-form">
				<a id="chat-link" class="chat-link focus-in-contract">
					<p class="chat-link-text"> Write to us, we are online! </p>
					<img src="../assets/img/leave.png" class="chat-link-img"
				/></a>

				<div id="chat" class="chat hidden">
					<div class="chat-messages">sdadas</div>
					<div class="chat-textarea-wrapper"><textarea></textarea></div>
				</div>
			</div> -->

    <div class="exchange-container width75persent">
      <h1>Currency exchange</h1>

      <p>Exchange Tron TRX to Dash DASH</p>

      <div class="attention-container">
        <h5>Attention</h5>
        <p>
          The exchange rate is not fixed and is calculated at the time of
          crediting coins to the wallet of the exchange service, the payment is
          usually made within 15 minutes during working hours (from 08-00 to
          24-00 GMT +3).
        </p>
      </div>

      <form class="exchange-form form confirmation">
        <h2>Send</h2>

        <div class="exchange-form-row">
          <div class="confirmation-amount">
            <p>Amount: <span class="grey">30000 Tron TRX</span></p>
          </div>
          <div class="currency-title-and-logo">
            <img src="../assets/img/tron.png" /><span>Tron TRX</span>
          </div>
        </div>

        <h2>Recieve</h2>

        <div class="exchange-form-row">
          <div class="confirmation-amount">
            <p>Amount: <span class="grey">30000 Tron TRX</span></p>
            <p>Into account: <span>XuAJExJ3fQV9xK7BgK8qAtX49XrqoMberf</span></p>
          </div>
          <div class="currency-title-and-logo">
            <img src="../assets/img/tron.png" /><span>Tron TRX</span>
          </div>
        </div>

        <h2>Personal data</h2>

        <div class="confirmation-amount">
          <p>E-mail: <span>relaxingsoundsforbody@gmail.com</span></p>
        </div>

        <button
          @click="$router.push('/preview/create-order/ready-order')"
          class="exchange-btn background-blue"
          type="button"
        >
          Create Order
        </button>

        <div class="exchange-form-final-text">
          <input type="checkbox" />
          <span
            >I read and agree with
            <a href="index.html" class="green">the terms and conditions</a>
          </span>
        </div>
      </form>
    </div>
  </section>
</template>

<script>
export default {};
</script>

<style scoped></style>
