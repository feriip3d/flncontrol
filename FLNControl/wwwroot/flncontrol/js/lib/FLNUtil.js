let FLNUtil = {
    isEmpty: (item) => {
        if (typeof item == 'string')
            item = item.trim();

        return item === '' || item === undefined || item === [];
    },
}

export { FLNUtil }