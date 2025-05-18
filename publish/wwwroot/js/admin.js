function slugify(text) {
    return text.toString().toLowerCase()
        .replace(/ç/g, 'c').replace(/ğ/g, 'g')
        .replace(/ı/g, 'i').replace(/ö/g, 'o')
        .replace(/ş/g, 's').replace(/ü/g, 'u')
        .replace(/[^a-z0-9\s-]/g, '') // özel karakterleri sil
        .replace(/\s+/g, '-')        // boşlukları tire yap
        .replace(/-+/g, '-')         // ardışık tireleri tek tire yap
        .replace(/^-+|-+$/g, '');    // baştaki ve sondaki tireleri kaldır
}