<template>
  <div id="app">
    <router-view/>
  </div>
</template>

<script>


export default {
  name: 'app',
}
</script>

<style lang="scss">
// #app {
//   font-family: 'Avenir', Helvetica, Arial, sans-serif;
//   -webkit-font-smoothing: antialiased;
//   -moz-osx-font-smoothing: grayscale;
//   text-align: center;
//   color: #2c3e50;
// }
// #nav {
//   padding: 30px;
//   a {
//     font-weight: bold;
//     color: #2c3e50;
//     &.router-link-exact-active {
//       color: #42b983;
//     }
//   }
// }
@import url('https://fonts.googleapis.com/css?family=Noto+Sans+TC');

@font-face {
  font-family: "TradeGothicforNike365-BdCn";
  src: url("./assets/fonts/TradeGothicforNike365-BdCn.eot");
  src:
  url("./assets/fonts/TradeGothicforNike365-BdCn.woff") format("woff"),
  url("./assets/fonts/TradeGothicforNike365-BdCn.ttf") format("truetype"),
  url("./assets/fonts/TradeGothicforNike365-BdCn.svg#filename") format("svg");
}

#app{
  color: #505050;
  font-family: Helvetica, 'Noto Sans TC', Arial, sans-serif;
  font-size: 16px;
  p{
    line-height: 1.5em;
  }
}

.nav-btn{
  color: #505050;
  &:hover {
    color: #505050;
    text-decoration: none;
  }
}

.sec {
  margin-top: 5em;
  margin-bottom: 5em;
  &__title{
    color: #3349a2;
    font-size: 2.375em;
    font-weight: bold;
    font-family: 'TradeGothicforNike365-BdCn', 'Noto Sans TC', Arial, sans-serif;
    &--secound{
      color: #000;
      font-size: 1.5em;
      font-family: 'TradeGothicforNike365-BdCn', 'Noto Sans TC', Arial, sans-serif;
    }

  }
}

.class-info{
  &__field{
    font-weight: bold;
  }
  &__btn-apply{
    border: 1px solid #555454;
    color: #555454 !important;
    font-weight: bold;
  }
}

.field-required{
  color: red;
}
</style>
