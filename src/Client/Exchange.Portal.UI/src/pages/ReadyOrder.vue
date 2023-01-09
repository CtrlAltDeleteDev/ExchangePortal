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
          <h2>How to make payment</h2>

          <div class="make-payment-guide">
            <p>Log in to your Tron Trx wallet/;</p>
            <p>Transfer the following amount to the wallet/</p>
            <p class="font-weigth500">—————————————————————</p>
            <p class="font-weigth500 margin-left20">
              TXhS5MBSwG7RmyZPFUnj3BeU2CJ4jycrSn
            </p>
            <p class="font-weigth500">—————————————————————</p>
            <p>Click on the button “I paid the application”/;</p>
            <p>Expect application processing by the operator/.</p>
          </div>

          <div class="payment-amount margin-top-15">
            <p>Amount of payment: <span>30000</span> Tron TRX</p>
          </div>

          <div class="payment-amount margin-top-15">
            <p>Amount to receive: <span>37.83</span> Dash DASH</p>
          </div>

          <p class="margin-top-20 payment-warning">
            <span class="red">Please be careful!</span> All fields must be
            filled in accordance with the instructions. Otherwise, the payment
            may be cancelled.
          </p>
        </div>

        <div class="payment-status-container">
          <div class="payment-amount width75persent margin-top-15">
            <p>Creation time: <span>27.11.2022, 14:01</span></p>
          </div>

          <div class="payment-amount width75persent margin-top-15">
            <p>
              Status of order:
              <span>Accepted, waiting to be paid by client</span>
            </p>
          </div>
        </div>

        <div class="payment-btn-container">
          <button
            @click="$router.push('/preview/create-order/ready-order/cancel')"
            class="exchange-btn background-red"
            type="button"
          >
            Cancel an order
          </button>
          <button
            @click="$router.push('/preview/create-order/ready-order/paid')"
            class="exchange-btn background-green"
            type="button"
          >
            Paid
          </button>
        </div>

        <div class="payment-bottom-box"></div>
      </form>
    </div>
  </section>
</template>

<script>
export default {};
</script>

<style scoped></style>
