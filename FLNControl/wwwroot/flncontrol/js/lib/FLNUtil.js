let FLNUtil = {
    isEmpty: (item) => {
        item = item.trim();
        return item === '' || item === undefined || item === [];
    },
}

export { FLNUtil }