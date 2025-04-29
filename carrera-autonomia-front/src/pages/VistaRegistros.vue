<template>
    <q-page class="q-pa-md">
        <q-card class="shadow-2" style="max-width: 1200px; margin: auto">
            <div class="bg-white">
                <q-img src="/LogCDAG.jpeg" alt="Banner CDAG"
                    style="border-top-left-radius: 16px; border-top-right-radius: 16px; max-height: 300px"
                    fit="cover" />
            </div>
            <q-card-section class="text-h6 text-primary text-center">
                TABLA REGISTROS DE PARTICIPANTES
            </q-card-section>

            <q-card-section class="q-gutter-sm">
                <q-input v-model="filtro" label="Buscar por Nacionalidad o ID Categoría" outlined dense debounce="300"
                    clearable color="primary" />

                <q-btn icon="file_download" color="green" label="Descargar Excel" @click="descargarExcel" />
            </q-card-section>

            <q-table :rows="registrosFiltrados" :columns="columnas" row-key="id" flat bordered class="q-mb-md"
                :loading="cargando" loading-label="Cargando registros...">
                <template v-slot:top-right>
                    <q-btn dense flat icon="refresh" @click="obtenerRegistros" label="Actualizar" />
                </template>

                <template v-slot:body-cell-fechaNacimiento="props">
                    <q-td :props="props">{{ formatearFecha(props.row.fechaNacimiento) }}</q-td>
                </template>

                <template v-slot:body-cell-fechaRegistro="props">
                    <q-td :props="props">{{ formatearFechaHora(props.row.fechaRegistro) }}</q-td>
                </template>

                <template v-slot:body-cell-esMayorEdad="props">
                    <q-td :props="props">{{ props.row.esMayorEdad ? 'Sí' : 'No' }}</q-td>
                </template>
            </q-table>
        </q-card>
    </q-page>
</template>




<script setup>
import { ref, computed, onMounted } from 'vue'
import axios from 'axios'
import * as XLSX from 'xlsx'

const registros = ref([])
const cargando = ref(false)
const filtro = ref('')

const columnas = [
    { name: 'id', label: 'ID', field: 'id', align: 'left' },
    { name: 'nombreCompleto', label: 'Nombre', field: 'nombreCompleto', align: 'left' },
    { name: 'correo', label: 'Correo', field: 'correo', align: 'left' },
    { name: 'identificacion', label: 'CUI/Pasaporte', field: 'identificacion', align: 'left' },
    { name: 'nacionalidad', label: 'Nacionalidad', field: 'nacionalidad', align: 'left' },
    { name: 'categoria', label: 'ID Categoría', field: 'categoria', align: 'left' },
    { name: 'esMayorEdad', label: 'Mayor de Edad', field: 'esMayorEdad', align: 'left' },
    { name: 'fechaNacimiento', label: 'Nacimiento', field: 'fechaNacimiento', align: 'left' },
    { name: 'fechaRegistro', label: 'Registrado', field: 'fechaRegistro', align: 'left' }
]

const obtenerRegistros = async () => {
    cargando.value = true
    try {
        const res = await axios.get('https://localhost:7046/api/registros/admin/registros')
        registros.value = res.data
    } catch (err) {
        console.error('Error cargando registros:', err)
    } finally {
        cargando.value = false
    }
}

const registrosFiltrados = computed(() => {
    const f = filtro.value.toLowerCase()
    return registros.value.filter(r =>
        r.nacionalidad?.toLowerCase().includes(f) || r.categoria?.toString().includes(f)
    )
})

function formatearFecha(fechaIso) {
    const fecha = new Date(fechaIso)
    return fecha.toLocaleDateString('es-ES')
}

function formatearFechaHora(fechaIso) {
    const fecha = new Date(fechaIso)
    return fecha.toLocaleString('es-ES')
}

// Función para generar y descargar archivo Excel
function descargarExcel() {
    const datos = registrosFiltrados.value.map(r => ({
        ID: r.id,
        Nombre: r.nombreCompleto,
        Correo: r.correo,
        CUI_Pasaporte: r.identificacion,
        Nacionalidad: r.nacionalidad,
        Categoria: r.categoria,
        Mayor_de_Edad: r.esMayorEdad ? 'Sí' : 'No',
        Fecha_Nacimiento: formatearFecha(r.fechaNacimiento),
        Fecha_Registro: formatearFechaHora(r.fechaRegistro)
    }))

    const hoja = XLSX.utils.json_to_sheet(datos)
    const libro = XLSX.utils.book_new()
    XLSX.utils.book_append_sheet(libro, hoja, 'Registros')

    XLSX.writeFile(libro, 'RegistrosCarrera.xlsx')
}

onMounted(obtenerRegistros)
</script>