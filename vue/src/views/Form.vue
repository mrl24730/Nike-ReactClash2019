<template>

<div id="cust">
  <div class="container-fluid">
      <div class="row justify-content-between align-item-center top-bar">
        <div class="col-auto ml-md-5">
          <img src="../assets/logo.png" alt="" class="img-fluid">
        </div>
        <div class="col-auto mr-md-5 align-self-center top-nav">
        </div>
      </div>
    </div>
  <div class="form">
    <div class="container-fluid text-center px-0">
      <div class="row no-gutters">
        <div class="col">
          <img src="../assets/banner5_1.jpg" class="img-fluid">
        </div>
      </div>
    </div>

    <div class="container text-center">
      <div class="sec" v-show="!submit_success">
        <div class="row mb-0">
          <div class="col">
            <h3 class="sec__title mb-5" v-html="$t('message.apply_form.event_summary')"></h3>
            <!-- <p v-html="$t('message.apply_form.event_summary2')"></p> -->
          </div>
        </div>
        <div>
          <div class="row">
            <div class="col">
              <img src="../assets/body_2.jpg" alt="" class="img-fluid">
            </div>
          </div>
        </div>
      </div>

      <div class="sec" v-show="!submit_success">
        <div class="row mb-4">
          <div class="col">
            <h3 class="sec__title" v-html="$t('message.apply_form.event_title')"></h3>
            <!-- <h3 class="sec__title--secound" v-html="$t('message.apply_form.event_sub_title')"></h3> -->
          </div>
        </div>
        <div class="row mt-2 mb-5">
          <div class="col">
            <p v-html="$t('message.apply_form.event_txt_1')"></p>
          </div>
        </div>
        <div class="row mb-4">
          <div class="col">
            <h3 class="sec__title" v-html="$t('message.apply_form.event_title2')"></h3>
            <!-- <h3 class="sec__title--secound" v-html="$t('message.apply_form.event_sub_title')"></h3> -->
          </div>
        </div>
        <div class="row mt-2">
          <div class="col">
            <p v-html="$t('message.apply_form.event_txt_2')"></p>
          </div>
        </div>
        <div class="row mt-3">
          <div class="col text-left">
            <p class="font-weight-bold">條款及細則</p>
            <ol>
              <li>本活動由 NIKE HONG KONG LIMITED (香港)有限公司主辦。</li>
              <li>活動名額有限，成功獲選的會員將會於2019年7月18日或之前收到確認電郵通知。</li>
              <li>請於報名時輸入有效的香港NIKEPLUS登記電郵，以便收取成功獲選通知。</li>
              <li>活動只限NIKEPLUS會員報名，如你還未是NIKEPLUS會員，請在<a href="https://www.nike.com.hk/memberShip/home.htm" target="_blank">這裡</a>登記，並在此表格報名。</li>
              <li>每個有效的香港 NIKEPLUS 登記電郵只限報名一次，每次只限一人。如多次登記，會以最先登記爲準。</li>
              <li>如有任何爭議，NIKE HONG KONG LIMITED (香港)將保留最終決定權。</li>
            </ol>
          </div>
        </div>
      </div>

      <div class="row mb-4 pt-5">
        <div class="col">
          <!-- <img src="../assets/body_2.png" alt="" class="img-fluid"> -->
          <h3 class="sec__title h2 font-weight-bold">多謝你的支持！</h3>
        </div>
      </div>
      <div class="row mb-5 pb-5">
        <div class="col">
            <p class="h4">此活動名額已滿，多謝大家踴躍登記，我們將不時舉辦更多精彩活動，請密切留意！</p>
        </div>
      </div>
    </div>

  </div>
</div>
</template>

<style scoped lang="scss">
.container{
  max-width: 790px;
}

.form-group{
  max-width: 600px;
  margin-left: auto;
  margin-right: auto;
}

input[type="radio"].gender + label{
  border: 1px solid gray;
  cursor: pointer;
  width: 50%;
}

input[type="radio"].gender:checked + label{
  border: 2px solid black;
}

.sec_remark{
  color: #9e9e9e;
}

.btn_apply{
  background: #000;
  color: #fff;
  font-weight: bold;
}

.spot_list{
  padding-left: 20px;
  li{
    list-style-type: none;
  }
}

.btm-checkbox{
  background: #e1e1e1;
  border: 1px solid #000;
  margin: 5em 0;
}
.custom-control-input:checked~.custom-control-label::before{
  background-color: #000;
  border-color: #000;
}

.err-list{
  color: red;
  &__item{
    list-style-type: none;
  }
}
</style>

<script>
import axios from 'axios'
import { LanguageSwitcher } from '../components/mixins/languageChangeMixin.js'
import moment from 'moment'

      // let localStr = 'zh-hk'
      // moment.locale('zh-hk')
      // console.log(moment('2019-12-02').format('dddd, MoDo'))
      // moment.locale('en')
      // console.log(moment('2019-12-02').format('D MMM Y (ddd)'))

export default {
  mixins: [LanguageSwitcher],
  name: 'cust',
  data: function () {
    return {
      multiLang: true,
      submit_success: false,
      error: [],
      name: '',
      phone: null,
      email: null,
      dob: null,
      ismale: null,
      tnc: false,
      edm: false,
      class_selected: null,
      date_selected: -1,
      class_opt: {},
      is_loading: false
    }
  },
  filters: {
    lowercase: function (value) {
      if (!value) return ''
      return value.toLowerCase()
    }
  },
  directives: {
    lowercase: {
      update: function (el) {
        el.value = el.value.toLowerCase()
      }
    },
    numberonly: {
      update: function (el, binding) {
        if (isNaN(parseInt(el.value))) {
          el.value = ''
          return
        }
        el.value = parseInt(el.value)
      }
    }
  },
  methods: {
    fetchTimeSlot () {
      axios
        .get('api/timeslot')
        .then(response => {
          // console.log(response)
          if (response.status === 200 && response.data.Code === 0) {
            this.class_opt = response.data.Data
          }
        })
        .catch(error => {
          console.log(error)
        })
    },
    isNumber: function (evt) {
      evt = (evt) ? evt : window.event
      var charCode = (evt.which) ? evt.which : evt.keyCode
      var valLength = evt.target.value.length
      var canInsert = valLength < 8
      if ((charCode > 31 && (charCode < 48 || charCode > 57)) || !canInsert) {
        evt.preventDefault()
      } else {
        return true
      }
    },
    submit () {
      if (this.is_loading) return

      this.checkForm()

      if (this.error.length > 0) {
        return
      }

      let formData = this.composeRecordPayload()

      this.is_loading = true
      axios({
        method: 'post',
        url: 'api/record',
        data: formData
      }).then(res => {
        if (res.status !== 200) {
          this.error.push(this.$i18n.t('message.apply_form.error_msg.unknow'))
          return
        }

        this.recordApiCallback(res.data)
      }).catch(error => {
        this.error.push(this.$i18n.t('message.apply_form.error_msg.unknow'))
        console.log(error)
      }).then(res => {
        this.is_loading = false
        this.fetchTimeSlot()
      })
    },
    composeRecordPayload () {
      let formData = {
        name: this.name,
        phone: this.phone,
        email: this.email,
        tid: this.class_selected,
        subscribe: this.edm,
        lang: this.$i18n.locale === 'tc'
      }

      if (this.dob) {
        var formatedDate = this.dob.substr(0, 2) + '-' + this.dob.substr(2, 2) + '-' + this.dob.substr(4, 4)
        // console.log(formatedDate)
        formData['birth'] = formatedDate
      }

      if (this.ismale >= 0) {
        formData['ismale'] = this.ismale
      }

      return formData
    },
    recordApiCallback (res) {
      switch (res.Code) {
        case 0:
          this.submit_success = true
          break
        case 1:
        case 2:
          this.error.push(this.$i18n.t('message.apply_form.error_msg.unknow'))
          break
        case 3:
          this.error.push(this.$i18n.t('message.apply_form.error_msg.phone_exist'))
          break
      }
    },
    date_opt_str (val) {

      let dateAry = val.split('-')
      return `${dateAry[1]}-${dateAry[0]}-2019`

      // let formatStr = ''
      // switch (this.$i18n.locale){
      //   case 'en':
      //     moment.locale('en')
      //     formatStr = 'D MMM Y (ddd)'
      //     break
      //   case 'tc':
      //     moment.locale('zh-hk')
      //     formatStr = 'dddd, MoDo'
      //     break
      // }


      // let localStr = 'zh-hk'
      // moment.locale('zh-hk')
      // console.log(moment('2019-12-02').format('dddd, MoDo'))
      // moment.locale('en')
      // console.log(moment('2019-12-02').format('D MMM Y (ddd)'))

    },
    checkForm () {
      console.log(this.class_opt);
      this.error = []

      if (!this.isNameValid()) {
        this.error.push(this.$i18n.t('message.apply_form.error_msg.name'))
      }

      if (!this.isPhoneValid()) {
        this.error.push(this.$i18n.t('message.apply_form.error_msg.phone'))
      }

      if (!this.isEmailValid()) {
        this.error.push(this.$i18n.t('message.apply_form.error_msg.email'))
      }

      if (!this.isDobValid()) {
        this.error.push(this.$i18n.t('message.apply_form.error_msg.dob'))
      }

      if (!this.isTidValid()) {
        this.error.push(this.$i18n.t('message.apply_form.error_msg.spot'))
      }

      if (!this.isTncAgree()) {
        this.error.push(this.$i18n.t('message.apply_form.error_msg.tnc'))
      }
    },
    isNameValid () {
      return this.name.length > 0
    },
    isPhoneValid () {
      return this.phone > 10000000 && this.phone < 99999999
    },
    isEmailValid () {
      let reg = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
      return reg.test(this.email)
    },
    isDobValid () {
      if (!this.dob) return true

      let reg = /[0-1]\d[0-3]\d\d{4}$/
      return reg.test(this.dob)
    },
    isTidValid () {
      return Boolean(this.class_selected)
    },
    isTncAgree () {
      return Boolean(this.tnc)
    }
  },
  watch: {
    date_selected (val) {
      this.date_selected = val
      this.class_selected = null
    }
  },
  computed: {
    date_opt () {
      let dateOpts = []

      for (var i = 0; i < this.class_opt.length; i++) {
        let dateKey = this.class_opt[i].Slot_group
        if (!dateOpts.includes(dateKey)) {
          dateOpts.push(dateKey)
        }
      }

      return dateOpts
    },
    class_opt_by_date () {
      var classes = []

      if (!this.date_selected) {
        return []
      }
      // console.log(this.date_selected)

      for (var i = 0; i < this.class_opt.length; i++) {
        if (this.class_opt[i].Slot_group === this.date_selected) {
          classes.push(this.class_opt[i])
        }
      }

      return classes
    }
  },
  mounted () {
    this.fetchTimeSlot()

    this.$ga.page({
      page: '/',
      title: 'Home',
      location: window.location.href
    })
  }
}
</script>
