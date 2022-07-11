<template>
  <input type="text" id="mobi">
</template>

<script>
import $ from 'jquery'
export default {
  name: 'mobiScroll',
  mounted: function() {
    console.log($(this.$el))

  },
  beforeDestroy: function() {  }
}
</script>
