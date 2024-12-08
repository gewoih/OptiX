<script setup lang="ts">
import 'ag-charts-enterprise'
import { AgFinancialCharts } from 'ag-charts-vue3'
import { marketDataService } from '@/services/marketDataService.ts'
import { ref, onMounted } from 'vue'

const options = ref({ data: [] })

const getData = async () => {
  const data = await marketDataService.get()
  const formattedData = data.map(item => ({
    ...item,
    date: new Date(item.date)
  }))

  options.value = {
    data: formattedData
  }
}

onMounted(() => {
  getData()
})
</script>

<template>
  <ag-financial-charts :options="options" style="height: 600px" />
</template>
