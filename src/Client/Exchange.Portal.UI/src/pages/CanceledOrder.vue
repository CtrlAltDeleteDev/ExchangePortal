<template>
  <section class="welcome-section">
    <!-- <div class="chat-form">
				<a id="chat-link" class="chat-link focus-in-contract">
					<p class="chat-link-text"> Write to us, we are online! </p>
					<img src="./imgs/leave.png" class="chat-link-img"
				/></a>

				<div id="chat" class="chat hidden">
					<div class="chat-messages">sdadas</div>
					<div class="chat-textarea-wrapper"><textarea></textarea></div>
				</div>
			</div> -->

    <div class="exchange-container width75persent">
      <h1>Currency exchange</h1>

      <p>Order ID 40835</p>

      <div class="attention-container">
        <h5>Attention</h5>
        <p>
          The exchange rate is not fixed and is calculated at the time of
          crediting coins to the wallet of the exchange service, the payment is
          usually made within 15 minutes during working hours (from 08-00 to
          24-00 GMT +3).
        </p>
      </div>

      <form class="exchange-form form payment">
        <div style="padding: 20px">
          <h2>Refusal of payment</h2>

          <p class="margin-top-20 payment-warning">
            Payment on request has been returned to your wallet.
          </p>

          <div class="payment-amount margin-top-20">
            <p>Send: <span>30000 Tron TRX</span></p>
          </div>

          <div class="payment-amount margin-top-15 flex">
            <p>Receive: <span>37.83 Dash DASH</span></p>
            <p>,&nbsp;</p>
            <p>Into account: <span>XuAJExJ3fQV9xK7BgK8qAtX49XrqoMberf</span></p>
          </div>
        </div>

        <div class="payment-status-container">
          <div class="payment-amount width75persent margin-top-15">
            <p>Creation time: <span>27.11.2022, 14:01</span></p>
          </div>

          <div class="payment-amount width75persent margin-top-15">
            <p>Status of order: <span>User refused to make payment</span></p>
          </div>
        </div>

        <div class="payment-btn-container-center">
          <button
            @click="$router.push('/')"
            class="exchange-btn background-blue"
            type="button"
          >
            Enable refreshing
          </button>
        </div>
      </form>
    </div>
  </section>
</template>

<script>
export default {};
</script>

<style scoped></style>
