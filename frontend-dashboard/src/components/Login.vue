<template>
    <div class="login font-mono overflow-y-auto max-h-screen">
        <div class="grid min-h-screen md:min-h-screen sm:min-h-screen xs:min-h-auto gap-4 md:gap-2 
            xl:grid-cols-5 lg:grid-cols-6 md:grid-cols-1 sm:grid-cols-1 xs:grid-cols-1 ">

            <!-- Chart Section -->
            <div class="xl:col-span-2 lg:col-span-2 md:col-span-1 sm:col-span-1 p-2 ">
                <v-chart :option="chartOptions" autoresize class="w-full h-full min-h-[300px]" />
            </div>

            <!-- Right Section -->
            <div class="xl:col-span-3 lg:col-span-4 md:col-span-1 p-2 place-self-center mb-0 md:mb-16 sm:mb-16 xl:w-[100%] lg:w-[100%] md:w-[80%] sm:w-[90%] xs:w-[100%]">
                <div
                    class="card bg-base-200 shadow-sm xl:p-4 lg:p-4 md:p-2 sm:p-4 xs:p-4  xl:h-[38rem] lg:h-[35rem] md:h-auto sm:h-auto xs:h-auto grid gap-4 md:gap-2 xl:grid-cols-6 lg:grid-cols-10 md:grid-cols-6 sm:grid-cols-10 xs:grid-cols-4 ">
                    <!-- Carousel -->
                    <div class="card-carousel xl:col-span-2 lg:col-span-4 md:col-span-2 sm:col-span-4 xs: xs:col-span-2" >
                        <div class="top-card">
                            <div class="carousel overflow-x-auto scroll-smooth whitespace-nowrap w-full h-[100%]" id="carousel" ref="carouselRef">
                                <div id="taiwan" class="carousel-item w-full">
                                    <img src="@/assets/img/taiwan.jpeg" class="inline-block w-full h-full object-cover" loading="lazy" />
                                </div>  
                                <div id="suzhou" class="carousel-item w-full"   >
                                    <img src="@/assets/img/suzhou.jpeg" class="inline-block w-full h-full object-cover" loading="lazy" />
                                </div>
                                <div id="chongqing" class="carousel-item w-full">
                                    <img src="@/assets/img/chongqing.jpeg"
                                        class="inline-block w-full h-full object-cover" loading="lazy" />
                                </div>
                                <div id="chennai" class="carousel-item w-full">
                                    <img src="@/assets/img/india.jpeg" class="inline-block w-full h-full object-cover" loading="lazy" />
                                </div>
                                <div id="haiphong" class="carousel-item w-full">
                                    <img src="@/assets/img/vietnam.jpeg" class="inline-block w-full h-full object-cover" loading="lazy" />
                                </div>
                                <div id="johor" class="carousel-item w-full">
                                    <img src="@/assets/img/malaysia.jpeg"
                                        class="inline-block w-full h-full object-cover" loading="lazy" />
                                </div>
                                <div id="batam" class="carousel-item w-full">
                                    <img src="@/assets/img/indonesia.jpeg"
                                        class="inline-block w-full h-full object-cover" loading="lazy" />
                                </div>
                                <div id="mexico" class="carousel-item w-full">
                                    <img src="@/assets/img/mexico.jpg" class="inline-block w-full h-full object-cover" loading="lazy" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- Radio Selector -->
                    <div class="xl:col-span-1 lg:col-span-2 md:col-span-1 sm:col-span-2 xs:col-span-2 h-auto
 ">
                        <span class="text-white text-sm sm:text-base">Choose Site</span>
                        <div class="radio-container mt-2 flex flex-col gap-1">
                            <input type="radio" id="radio-taiwan" name="carousel" checked @change="scrollToItem('taiwan')">
                            <label for="radio-taiwan" data-target="taiwan">Taiwan, China</label>

                            <input type="radio" id="radio-suzhou" name="carousel" @change="scrollToItem('suzhou')">
                            <label for="radio-suzhou" data-target="suzhou">Suzhou, China</label>

                            <input type="radio" id="radio-chongqing" name="carousel" @change="scrollToItem('chongqing')">
                            <label for="radio-chongqing" data-target="chongqing">Chongqing, China</label>

                            <input type="radio" id="radio-india" name="carousel" @change="scrollToItem('chennai')">
                            <label for="radio-india" data-target="chennai">Chennai, India</label>

                            <input type="radio" id="radio-haiphong" name="carousel" @change="scrollToItem('haiphong')">
                            <label for="radio-haiphong" data-target="haiphong">Haiphong, Vietnam</label>

                            <input type="radio" id="radio-johor" name="carousel" @change="scrollToItem('johor')">
                            <label for="radio-johor" data-target="johor">Johor, Malaysia</label>

                            <input type="radio" id="radio-batam" name="carousel" @change="scrollToItem('batam')">
                            <label for="radio-batam" data-target="batam">Batam, Indonesia</label>

                            <input type="radio" id="radio-mexico" name="carousel" @change="scrollToItem('mexico')">
                            <label for="radio-mexico" data-target="mexico">Mexico</label>

                            <div class="glider-container">
                                <div class="glider"></div>
                            </div>
                        </div>
                    </div>

                    <!-- Form Section -->
                    <div class="card-body xl:col-span-3 lg:col-span-4 md:col-span-3 sm:col-span-4 xs:col-span-4 h-[80%]">
                        <h1 class="text-lg font-bold text-default">{{ localized.welcome }}</h1>
                        <fieldset class="fieldset bg-base-200 border border-base-300 p-4 mt-2">
                            <legend class="fieldset-legend">{{ localized.legend }}</legend>

                            <label class="floating-label">{{ localized.workId }}</label>
                            <input type="text" v-model="workId"
                                :class="['input w-[60%] xl:input-md input-sm ', errors.workId ? 'border-red-500' : '']" :placeholder="localized.workId" />
                            <p v-if="errors.workId" class="text-red-500 text-xs">Work ID is required.</p>

                            <label class="floating-label mt-3">{{ localized.name }}</label>
                            <input type="text" v-model="name"
                                :class="['input w-[60%] xl:input-md input-sm ', errors.name ? 'border-red-500' : '']" :placeholder="localized.name" />
                            <p v-if="errors.name" class="text-red-500 text-xs">Name is required.</p>

                            <label class="floating-label mt-3">{{ localized.password }}</label>
                            <input type="password" v-model="password" :class="['input w-[60%] xl:input-md input-sm ', errors.password ? 'border-red-500' : '']" :placeholder="localized.password" />
                            <p v-if="errors.password" class="text-red-500 text-xs">Password is required.</p> 

                            <div class="flex flex-row justify-between gap-8 w-full mt-4">
                                <button class="h-12 w-32" @click="login">
                                    {{ localized.login }}
                                </button>

                                <a class="underline cursor-pointer xl:text-sm lg:text-xs md:text-xs sm:text-xs xs:text-xs mt-3" @click="loginAsGuest">
                                    {{ localized.guest }}
                                </a>
                            </div>
                        </fieldset>
                    </div>
                </div>
            </div>

        </div>

    </div>
</template>
<script>
    import {
        ref
    } from 'vue'
    import * as echarts from 'echarts/core'
    import {
        TitleComponent,
        TooltipComponent,
        GeoComponent,
        VisualMapComponent
    } from 'echarts/components'

    import {
        MapChart,
        ScatterChart
    } from 'echarts/charts'
    import {
        CanvasRenderer
    } from 'echarts/renderers'

    echarts.use([
        TitleComponent,
        TooltipComponent,
        GeoComponent,
        VisualMapComponent,
        MapChart,
        ScatterChart,
        CanvasRenderer
    ])

    import worldMap from '@/assets/maps/world.json'

    export default {
        data() {
            return {
                workId: '',
                name: '',
                password: '',
                errors: {
                    workId: false,
                    name: false,
                    password: false
                },
                chartOptions: {},
                selectedCountryId: 'taiwan',
                localizedText: {
                    taiwan: {
                        welcome: 'Welcome! Please Login Your Account First.',
                        legend: 'Login',
                        workId: 'Work ID',
                        name: 'Name',
                        password: 'Password',
                        login: 'Login',
                        guest: 'Log in as Guest?',
                    },
                    suzhou: {
                        welcome: '欢迎！请先登录您的账户。',
                        legend: '登录',
                        workId: '工号',
                        name: '姓名',
                        password: '密码',
                        login: '登录',
                        guest: '以访客身份登录？'
                    },
                    chongqing: {
                        welcome: '欢迎来到重庆工厂，请先登录。',
                        legend: '登录',
                        workId: '工号',
                        name: '姓名',
                        password: '密码',
                        login: '登录',
                        guest: '以訪客身份登入？'
                    },
                    chennai: {
                        welcome: 'வணக்கம்! முதலில் உங்கள் கணக்கில் உள்நுழைக.',
                        legend: 'உள்நுழை',
                        workId: 'வேலை ஐடி',
                        name: 'பெயர்',
                        password: 'கடவுச்சொல்',
                        login: 'உள்நுழை',
                        guest: 'விருந்தினராக உள்நுழையலாமா?'
                    },
                    haiphong: {
                        welcome: 'Xin chào! Vui lòng đăng nhập trước.',
                        legend: 'Đăng nhập',
                        workId: 'Mã nhân viên',
                        name: 'Tên',
                        password: 'Mật khẩu',
                        login: 'Đăng nhập',
                        guest: 'Đăng nhập với tư cách khách?'
                    },
                    johor: {
                        welcome: 'Selamat datang! Sila log masuk dahulu.',
                        legend: 'Log Masuk',
                        workId: 'ID Pekerja',
                        name: 'Nama',
                        password: 'Katalaluan',
                        login: 'Log Masuk',
                        guest: 'Log masuk sebagai Tetamu?'
                    },
                    batam: {
                        welcome: 'Selamat datang! Silakan login terlebih dahulu.',
                        legend: 'Login',
                        workId: 'ID Pegawai',
                        name: 'Nama',
                        password: 'Kata Sandi',
                        login: 'Login',
                        guest: 'Masuk sebagai Tamu?'
                    },
                    mexico: {
                        welcome: '¡Bienvenido! Por favor inicie sesión primero.',
                        legend: 'Iniciar sesión',
                        workId: 'ID de empleado',
                        name: 'Nombre',
                        password: 'Contraseña',
                        login: 'Iniciar sesión',
                        guest: '¿Iniciar sesión como invitado?'
                    },
                },
            }
        },
        methods: {
            scrollToItem(itemId) {
                this.selectedCountryId = itemId;
                const target = document.getElementById(itemId)
                target ?.scrollIntoView({
                    behavior: 'smooth',
                    inline: 'start'
                })
                const url = new URL(window.location);
                url.searchParams.set('country', itemId);
                window.history.pushState({}, '', url);
            },
            login() {
                this.errors.workId = !this.workId;
                this.errors.name = !this.name;
                this.errors.password = !this.password;

                if (this.errors.workId || this.errors.name || this.errors.password) {
                    return;
                }

                localStorage.setItem('isLoggedIn', 'true');
                localStorage.setItem('userType', 'user');
                this.$emit('login-success', 'user');
                this.$router.push({
                    path: '/home',
                    query: {
                        view: 'home'
                    }
                });
            },
            loginAsGuest() {
                localStorage.setItem('isLoggedIn', 'true');
                localStorage.setItem('userType', 'guest');
                this.$emit('login-success', 'guest');
                this.$router.push({
                    path: '/home',
                    query: {
                        view: 'home'
                    }
                });
            }
        },
        computed: {
            localized() {
                return this.localizedText[this.selectedCountryId || this.localizedText.taiwan]
            }
        },
        mounted() {
            const carouselRef = ref(null)
            echarts.registerMap('world', worldMap)
            const active = ['cn', 'in', 'vn', 'my', 'id', 'mx', 'tw']
            const coords = {
                cn: {
                    lon: 100.86,
                    lat: 35.19,
                    name: 'China'
                },
                in: {
                    lon: 80.96,
                    lat: 20.59,
                    name: 'India'
                },
                vn: {
                    lon: 110.27,
                    lat: 14.06,
                    name: 'Vietnam'
                },
                my: {
                    lon: 101.98,
                    lat: 4.21,
                    name: 'Malaysia'
                },
                id: {
                    lon: 120.92,
                    lat: -4.79,
                    name: 'Indonesia'
                },
                mx: {
                    lon: -102.55,
                    lat: 23.63,
                    name: 'Mexico'
                },
                tw: {
                    lon: 125.00,
                    lat: 23.70,
                    name: 'Taiwan'
                }
            }

            const scatterPoints = active.map(code => ({
                name: coords[code].name,
                value: [coords[code].lon, coords[code].lat]
            }))

            this.chartOptions = {
                backgroundColor: 'transparent',
                geo: {
                    map: 'world',
                    roam: false,
                    silent: true,
                    selectedMode: false,
                    itemStyle: {
                        areaColor: 'transparent',
                        borderColor: '#aaa ',
                        shadowColor: 'rgba(255, 255, 255, 0.7)',
                    },
                    emphasis: {
                        itemStyle: {
                            areaColor: '#00d3bb'
                        }
                    }
                },
                series: [{
                        type: 'map',
                        map: 'world',
                        roam: false,
                        silent: true,
                        itemStyle: {
                            areaColor: 'transparent',
                            borderColor: 'transparent'
                        },
                        label: {
                            show: false
                        },
                        data: active.map(code => ({
                            name: coords[code].name,
                            itemStyle: {
                                areaColor: '#191e24',
                                borderColor: '#fff',
                                borderWidth: 1
                            }
                        }))
                    },
                    {
                        type: 'effectScatter',
                        coordinateSystem: 'geo',
                        data: scatterPoints,
                        symbolSize: 12,
                        showEffectOn: 'render',
                        rippleEffect: {
                            brushType: 'stroke',
                            scale: 3
                        },
                        itemStyle: {
                            color: '#6EB6C5',
                            shadowBlur: 10,
                            shadowColor: '#6EB6C5'
                        },
                        label: {
                            show: true,
                            formatter: '{b}',
                            color: '#fff',
                            fontSize: 20,
                            fontWeight: 'semibold'
                        }
                    }
                ]
            }
        }
    };
</script>

<style scoped>
    html[data-theme='light'] .login {
        background-color: #eeeeee;
    }

    html[data-theme='light'] .card {
        background-color: #191e24;
    }

    html[data-theme='light'] .fieldset {
        background-color: #191e24;
        border: 1px solid #1d232a;
        color: #fff;
    }

    html[data-theme='light'] .fieldset-legend {
        color: #fff;
    }

    html[data-theme='light'] input {
        border: 1px solid #0000;
        background-color: oklch(25.33% 0.016 252.42);
    }

    .map-container {
        position: relative;
        width: 100%;
        max-width: 800px;
        margin: auto;
        border-radius: 8px;
        overflow: hidden;
        box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
    }

    .card-carousel {
        position: relative;
        background-color: #fff;
        border-radius: 20px;
        /*box-shadow: 0px 5px 5px #313131;*/
        overflow: hidden;
        /* height: 90%; */
    }

    .top-card {
        border-top-left-radius: 20px;
        border-top-right-radius: 20px;
        height: 100%;
        transition: height 0.3s ease;
        background-color: #fff;
    }

    .card-content {
        padding-top: 13%;
        display: flex;
        justify-content: center;
        align-items: center;
        color: #fff;
    }

    .card-title {
        font-weight: 700;
        font-size: 18px;
    }

    .card-txt {
        font-size: 14px;
    }

    .card-btn {
        font-size: 13px;
        margin-top: 15%;
        text-decoration: none;
        color: white;
        background-color: transparent;
        border: solid 2px #fff;
        border-radius: 15px;
        padding: 5%;
    }

    .radio-container {
        position: relative;
        /* height: 100%; */
        display: flex;
        flex-direction: column;
        padding-left: 0.5rem;
        font-size: 13px;
    }

    .radio-container input {
        appearance: none;
        position: absolute;
        opacity: 0;
    }

    .radio-container label {
        display: block;
        padding: 1rem;
        color: gray;
    }

    .radio-container input:checked+label {
        color: #6EB6C5;
    }

    .glider-container {
        position: absolute;
        left: 0;
        top: 0;
        bottom: 0;
        width: 2px;
        background: rgba(0, 0, 0, 0.2);
    }

    .glider {
        position: relative;
        height: calc(100% / 8);
        width: 100%;
        background: #6EB6C5;
        transition: transform 0.3s ease;
    }

    input#radio-taiwan:checked~.glider-container .glider {
        transform: translateY(0%);
    }

    input#radio-suzhou:checked~.glider-container .glider {
        transform: translateY(100%);
    }

    input#radio-chongqing:checked~.glider-container .glider {
        transform: translateY(200%);
    }

    input#radio-india:checked~.glider-container .glider {
        transform: translateY(300%);
    }

    input#radio-haiphong:checked~.glider-container .glider {
        transform: translateY(400%);
    }

    input#radio-johor:checked~.glider-container .glider {
        transform: translateY(500%);
    }

    input#radio-batam:checked~.glider-container .glider {
        transform: translateY(600%);
    }

    input#radio-mexico:checked~.glider-container .glider {
        transform: translateY(700%);
    }

    button {
        padding: 1em;
        overflow: hidden;
        border-radius: 10px;
        color: #fff;
        background: none;
        position: relative;
        /* padding-bottom: 3em; */
        cursor: pointer;
        box-shadow: 0 0 1em .25em var(--glow-color),
            0 0 4em 1em var(--glow-spread-color),
            inset 0 0 .75em .25em var(--glow-color);
        --glow-color: rgb(176, 255, 248);
        --glow-spread-color: rgba(123, 255, 237, 0.781);
        --enhanced-glow-color: rgb(206, 255, 251);
        --btn-color: rgb(95, 186, 189);
    }
</style>