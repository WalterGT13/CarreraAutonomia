<template>
  <q-page class="q-pa-md bg-grey-2">
    <q-card class="shadow-2 bordered q-pa-none" style="max-width: 800px; margin: auto; border-radius: 16px;">
      <!-- Banner -->
      <div class="bg-white">
        <q-img src="/LogCDAG.jpeg" alt="Banner CDAG"
          style="border-top-left-radius: 16px; border-top-right-radius: 16px; max-height: 220px" fit="cover" />
      </div>

      <!-- Título -->
      <q-card-section class="q-px-lg text-center">
        <div class="text-h6 text-primary text-bold">
          REGISTRO CARRERA AUTONOMÍA DEL DEPORTE 2025
        </div>
        <div class="text-body2 q-mt-xs">
          La Confederación Deportiva Autónoma de Guatemala te invita a la Carrera por la Autonomía del Deporte el
          <strong>01 de Junio de 2024</strong> a las 8:00 horas. Punto de salida: Debajo del Puente Olímpico, zona 5.
        </div>
      </q-card-section>

      <!-- Formulario -->
      <q-form @submit.prevent="registrar" class="q-gutter-md q-pa-lg">
        <q-input filled v-model="form.nombreCompleto" label="Nombres y Apellidos *" color="primary" bg-color="white"
          :rules="[val => !!val || 'Campo obligatorio']" />

        <q-input filled v-model="form.correo" label="Correo electrónico *" type="email" color="primary" bg-color="white"
          :rules="[val => !!val || 'Campo obligatorio']" />

        <q-input filled v-model="form.fechaNacimiento" type="date" label="Fecha de nacimiento *" color="primary"
          bg-color="white" @update:model-value="validarEdad" :rules="[val => !!val || 'Campo obligatorio']" />

        <q-input filled label="¿Es mayor de edad?" :model-value="form.esMayorEdad ? 'Sí' : 'No'" disable
          bg-color="grey-3" color="grey-8" />

        <div class="text-subtitle2 text-primary">
          ¿Eres residente nacional o extranjero? <span class="text-negative">*</span>
        </div>
        <q-option-group v-model="form.nacionalidad" :options="[
          { label: 'Nacional', value: 'Nacional' },
          { label: 'Extranjero', value: 'Extranjero' }
        ]" type="radio" color="primary" />

        <q-input filled v-model="form.identificacion" label="CUI (Nacional) / Pasaporte (Extranjero) *" color="primary"
          bg-color="white" :rules="[val => !!val || 'Campo obligatorio']" />

        <div class="text-subtitle2 q-mb-xs text-primary">
          Categoría <span class="text-negative">*</span>
        </div>
        <q-option-group v-model="form.categoria" :options="[
          { label: '4km', value: 1 },
          { label: '8km', value: 2 },
          { label: '12km', value: 3 }
        ]" type="radio" color="primary" class="q-mb-md" />

        <!-- Banner de condiciones -->
        <q-banner class="bg-grey-1 text-body2 rounded-borders q-pa-md">
          <strong>CARRERA DE LA AUTONOMÍA DEL DEPORTE</strong><br><br>
          Yo, participante de la carrera que se llevará a cabo en la ciudad de Guatemala el
          <strong>01 de Junio de 2024</strong>, por este medio <strong>afirmo y RATIFICO</strong> que:
          <ul class="q-ml-md">
            <li>Participaré usando la playera asignada ({{ camisasDisponibles }} unidades disponibles).</li>
            <li>Estoy física y mentalmente preparado/a para el evento.</li>
            <li>Participo bajo mi responsabilidad, eximiendo a la organización de cualquier reclamo.</li>
            <li>Acepto el uso de mis datos e imagen con fines informativos y promocionales.</li>
          </ul>
        </q-banner>

        <!-- Alerta si no hay camisas -->
        <q-banner v-if="camisasDisponibles === 0" class="bg-red-4 text-white q-mt-sm">
          Atención: Lo sentimos, ya no hay camisas disponibles para el evento. El registro ha sido cerrado.
        </q-banner>

        <q-checkbox v-model="form.aceptaCondiciones" label="Aceptar condiciones *" class="text-primary" />

        <q-btn label="REGISTRAR" type="submit" color="primary" glossy class="full-width q-mt-sm"
          :disable="camisasDisponibles === 0" />

        <q-banner v-if="mensaje" dense :class="mensajeTipo" class="q-mt-md text-white text-center q-pa-sm">
          {{ mensaje }}
        </q-banner>
      </q-form>
    </q-card>
  </q-page>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import axios from 'axios'

// Formulario
const form = ref({
  nombreCompleto: '',
  correo: '',
  fechaNacimiento: '',
  esMayorEdad: false,
  nacionalidad: '',
  identificacion: '',
  categoria: '',
  aceptaCondiciones: false
})

const mensaje = ref('')
const mensajeTipo = ref('')
const camisasDisponibles = ref(0)

function validarEdad() {
  if (!form.value.fechaNacimiento) return
  const nacimiento = new Date(form.value.fechaNacimiento)
  const hoy = new Date()
  let edad = hoy.getFullYear() - nacimiento.getFullYear()
  const cumpleEsteAno = hoy < new Date(hoy.getFullYear(), nacimiento.getMonth(), nacimiento.getDate())
  if (cumpleEsteAno) edad--
  form.value.esMayorEdad = edad >= 18
}

async function obtenerDisponibles() {
  try {
    const res = await axios.get('https://localhost:7046/api/registros/disponibles')
    camisasDisponibles.value = res.data
  } catch (err) {
    console.error('Error al obtener camisas disponibles:', err)
  }
}

async function registrar() {
  if (
    !form.value.nombreCompleto ||
    !form.value.correo ||
    !form.value.fechaNacimiento ||
    !form.value.nacionalidad ||
    !form.value.identificacion ||
    !form.value.categoria
  ) {
    mensaje.value = 'Por favor completa todos los campos obligatorios.'
    mensajeTipo.value = 'bg-negative'
    return
  }

  if (!form.value.aceptaCondiciones) {
    mensaje.value = 'Debe aceptar las condiciones para continuar.'
    mensajeTipo.value = 'bg-negative'
    return
  }

  try {
    const res = await axios.post('https://localhost:7046/api/registros', form.value)
    mensaje.value = res.data
    mensajeTipo.value = 'bg-positive'
    await obtenerDisponibles()

    // Ocultar mensaje luego de 4s
    setTimeout(() => {
      mensaje.value = ''
      mensajeTipo.value = ''
    }, 4000)
  } catch (err) {
    mensaje.value = err.response?.data || 'Error inesperado'
    mensajeTipo.value = 'bg-negative'
  }
}

onMounted(() => {
  obtenerDisponibles()
})
</script>
