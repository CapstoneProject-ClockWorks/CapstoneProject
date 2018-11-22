requirejs.config({
    baseUrl: 'public',
    paths: {

    }
});
requirejs(['build/js/custom.min']);
requirejs(['build/js/common']);
requirejs(['build/js/fixHeight']);
requirejs(['build/js/include']);
