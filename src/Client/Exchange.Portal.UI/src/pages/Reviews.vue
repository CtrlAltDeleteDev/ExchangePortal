<template>
  <div class="">
    <section class="welcome-section" id="reviews">
			<div class="width75persent nunito">
				<div class="contacts-container">
					<h1>Reviews</h1>
					<div class="review-items">
						<div class="reviev-item">
							<div class="reviev-item-header">
								<p class="reviev-item-author">Kirill</p
								><p class="reviev-item-time">22.11.2022, 20:40</p>
							</div>
							<p class="reviev-item-content">Fast!</p>
						</div>

						<div class="reviev-item">
							<div class="reviev-item-header">
								<p class="reviev-item-author">Kirill</p
								><p class="reviev-item-time">22.11.2022, 20:40</p>
							</div>
							<p class="reviev-item-content">Fast!</p>
						</div>

						<div class="reviev-item">
							<div class="reviev-item-header">
								<p class="reviev-item-author">Kirill</p
								><p class="reviev-item-time">22.11.2022, 20:40</p>
							</div>
							<p class="reviev-item-content">Fast!</p>
						</div>
					</div>

					<div class="review-pagination">
						<button class="review-pagination-item active-btn"><p>1</p></button>
						<button class="review-pagination-item"><p>2</p></button>
						<button class="review-pagination-item"><p>3</p></button>
						<button class="review-pagination-item active-btn"
							><p>...</p></button
						>
						<button class="review-pagination-item"><p>255</p></button>
						<button class="review-pagination-item"><p>256</p></button>
					</div>
				</div>

				<form class="review-form form">
					<h2>Post review</h2>

					<label
						>Your name <b class="red">*</b>:
						<input />
					</label>

					<label
						>Your e-mail <b class="red">*</b>:
						<input />
					</label>

					<label
						>Your e-mail <b class="red">*</b>:
						<textarea></textarea>
					</label>

					<p
						>I consent to processing of my personal data and accept the terms
						and conditions of the <a href=".">User Agreement</a>.</p
					>

					<button class="review-btn background-blue">Leave review</button>
				</form>
			</div>

			<div class="welcome-bottom-block">
				<div class="welcome-bottom-block-item">
					<img src="../assets/img/bitcoinCursor.png" />

					<h2>Choose currency</h2>
					<p>
						Operations on the service are carried out in automatic and
						semi-automatic modes, guaranteed time for processing applications is
						5-30 minutes
					</p>
				</div>

				<div class="welcome-bottom-block-arrow wide-descpot-element">
					<img src="../assets/img/arrow.png" />
				</div>

				<div class="welcome-bottom-block-item">
					<img src="../assets/img/phoneCircle.png" />

					<h2>Exchange</h2>
					<p>
						A full range of functions provides a safe conversion of electronic
						currencies, a convenient service that does not cause questions and
						difficulties when performing an exchange
					</p>
				</div>

				<div class="welcome-bottom-block-arrow wide-descpot-element">
					<img src="../assets/img/arrow.png" />
				</div>

				<div class="welcome-bottom-block-item">
					<img src="../assets/img/bitcoinWithHand.png" />

					<h2>Receive your funds</h2>
					<p>
						A full range of functions provides a safe conversion of electronic
						currencies, a convenient service that does not cause questions and
						difficulties when performing an exchange
					</p>
				</div>
			</div>
		</section>
  </div>
</template>

<script>
export default {};
</script>

<style scoped></style>
