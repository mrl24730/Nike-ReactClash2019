export const LanguageSwitcher = {
  methods: {
    changeLanguage: function (lang) {
      this.$i18n.locale = lang
    }
  }
}
